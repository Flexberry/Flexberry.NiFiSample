namespace IIS.NiFiSample
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using ICSSoft.Services;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.Audit;
    using ICSSoft.STORMNET.Business.Audit.Objects;
    using ICSSoft.STORMNET.Security;
    using IIS.Caseberry.Logging.Objects;
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NewPlatform.Flexberry.AuditBigData;
    using NewPlatform.Flexberry.AuditBigData.Serialization;
    using NewPlatform.Flexberry.ORM;
    using NewPlatform.Flexberry.ORM.ODataService.Extensions;
    using NewPlatform.Flexberry.ORM.ODataService.Files;
    using NewPlatform.Flexberry.ORM.ODataService.Model;
    using NewPlatform.Flexberry.ORM.ODataService.WebApi.Extensions;
    using NewPlatform.Flexberry.ORM.ODataServiceCore.Common.Exceptions;
    using NewPlatform.Flexberry.Services;
    using Unity;

    /// <summary>
    /// Класс настройки запуска приложения.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">An application configuration properties.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// An application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configurate application services.
        /// </summary>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </remarks>
        /// <param name="services">An collection of application services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            string connStr = Configuration["DefConnStr"];

            services.AddMvcCore(
                    options =>
                    {
                        options.Filters.Add<CustomExceptionFilter>();
                        options.EnableEndpointRouting = false;
                    })
                .AddFormatterMappings();

            services.AddOData();

            services.AddControllers().AddControllersAsServices();

            services.AddCors();
            services
                .AddHealthChecks()
                .AddNpgSql(connStr);
        }

        /// <summary>
        /// Configurate the HTTP request pipeline.
        /// </summary>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </remarks>
        /// <param name="app">An application configurator.</param>
        /// <param name="env">Information about web hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            LogService.LogInfo("Инициирован запуск приложения.");

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            app.UseODataService(builder =>
            {
                builder.MapFileRoute();

                var assemblies = new[]
                {
                    typeof(ObjectsMarker).Assembly,
                    typeof(ApplicationLog).Assembly,
                    typeof(UserSetting).Assembly,
                    typeof(Lock).Assembly,
                };
                var modelBuilder = new DefaultDataObjectEdmModelBuilder(assemblies, true);

                var token = builder.MapDataObjectRoute(modelBuilder);

                token.Functions.Register(new Func<bool>(AddTestData));

                token.Events.CallbackAfterInternalServerError = AfterInternalError;
            });
        }

        /// <summary>
        /// Configurate application container.
        /// </summary>
        /// <param name="container">Container to configure.</param>
        public void ConfigureContainer(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            // FYI: сервисы, в т.ч. контроллеры, создаются из дочернего контейнера.
            while (container.Parent != null)
            {
                container = container.Parent;
            }

            // FYI: сервис данных ходит в контейнер UnityFactory.
            container.RegisterInstance(Configuration);

            this.RegisterDataObjectFileAccessor(container);

            ISecurityManager emptySecurityManager = new EmptySecurityManager();

            // Регистрируем основной DataService.
            string mainConnectionString = Configuration.GetConnectionString("DefConnStr");
            IDataService mainDataService = new PostgresDataService(emptySecurityManager)
            {
                CustomizationString = mainConnectionString,
            };

            container.RegisterInstance<IDataService>(mainDataService, InstanceLifetime.Singleton);

            // Регистрируем DataService аудита.
            string auditConnectionString = Configuration.GetConnectionString("AuditConnString");

            IDataService auditDataServiceClickhouse = new ClickHouseDataService()
            {
                CustomizationString = auditConnectionString
            };
            container.RegisterInstance<IDataService>("auditDataService", auditDataServiceClickhouse, InstanceLifetime.Singleton);

            // Инициализируем сервис аудита.
            var auditAppSetting = new AuditAppSetting
            {
                AppName = "Test.audit",
                AuditEnabled = true,
            };

            ILegacyAuditSerializer auditSerializer = new JsonLegacyAuditSerializer();
            ILegacyAuditConverter auditConverter = new LegacyAuditConverter<JsonFieldAuditData>();
            IAudit audit = new LegacyAuditManager(container.Resolve<IDataService>("auditDataService"), auditConverter, auditSerializer);
            IAuditService auditService = new AuditService();

            AuditService.InitAuditService(auditAppSetting, audit, auditService);
        }

        /// <summary>
        /// Register implementation of <see cref="IDataObjectFileAccessor"/>.
        /// </summary>
        /// <param name="container">Container to register at.</param>
        private void RegisterDataObjectFileAccessor(IUnityContainer container)
        {
            const string fileControllerPath = "api/file";
            string baseUriRaw = Configuration["BackendRoot"];
            if (string.IsNullOrEmpty(baseUriRaw))
            {
                throw new System.Configuration.ConfigurationErrorsException("BackendRoot is not specified in Configuration or enviromnent variables.");
            }

            Console.WriteLine($"baseUriRaw is {baseUriRaw}");
            var baseUri = new Uri(baseUriRaw);
            string uploadPath = Configuration["UploadUrl"];
            container.RegisterSingleton<IDataObjectFileAccessor, DefaultDataObjectFileAccessor>(
                Invoke.Constructor(
                    baseUri,
                    fileControllerPath,
                    uploadPath,
                    null));
        }

        /// <summary>
        /// Метод, вызываемый после возникновения исключения.
        /// </summary>
        /// <param name="ex">Исключение, которое возникло внутри ODataService.</param>
        /// <param name="code">Возвращаемый код HTTP. По-умолчанияю 500.</param>
        /// <returns>Исключение, которое будет отправлено клиенту.</returns>
        public static Exception AfterInternalError(Exception ex, ref HttpStatusCode code)
        {
            var environmentVariable = Environment.GetEnvironmentVariables();
            return ex;
        }

        private bool AddTestData()
        {
            IUnityContainer unityContainer = UnityFactory.GetContainer();
            IDataService dataService = unityContainer.Resolve<IDataService>();

            Random rnd = new Random();

            List<Brand> brandList = new List<Brand>();
            for (int i = 0; i <= 100; i++)
            {
                int number = rnd.Next();
                Brand brend = new Brand { Name = $"Brand {number}" };
                brandList.Add(brend);
            }

            List<ProducingCountry> producingCountryList = new List<ProducingCountry>();
            for (int i = 0; i <= 100; i++)
            {
                int number = rnd.Next();
                ProducingCountry producingCountry = new ProducingCountry { Name = $"Country {number}" };
                producingCountryList.Add(producingCountry);
            }

            List<Car> carList = new List<Car>();
            for (int i = 0; i <= 20000; i++)
            {
                int number = rnd.Next();
                int numberBrand = rnd.Next(0, 100);
                CarType type = CarType.Coupe;
                switch (rnd.Next(0, 2))
                {
                    case 0:
                        type = CarType.Sedan;
                        break;
                    case 1:
                        type = CarType.Crossover;
                        break;
                }

                string monthInt = rnd.Next(1, 12).ToString();
                string month = monthInt.Length == 1 ? $"0{monthInt}" : monthInt;
                int countDay = month == "02" ? 28 : 30;
                string dayInt = rnd.Next(01, countDay).ToString();
                string days = dayInt.Length == 1 ? $"0{dayInt}" : dayInt;
                string dateStr = $"{days}.{month}.{rnd.Next(1980, 2022)}";
                DateTime carDate = DateTime.ParseExact(dateStr, "dd.MM.yyyy", null);

                Car car = new Car { CarNumber = $"CarNumber {number}", CarBody = type, Brand = brandList[numberBrand], CarDate = carDate };
                carList.Add(car);
            }

            List<SparePart> sparePartList = new List<SparePart>();
            for (int i = 0; i <= 40000; i++)
            {
                int number = rnd.Next();
                int carNumber = rnd.Next(0, 20000);
                int quantity = rnd.Next(1, 100000);
                bool used = rnd.Next(0, 1) == 0;

                SparePart sparePart = new SparePart { Name = $"SparePart {number}", Car = carList[carNumber], Quantity = quantity, Used = used };
                sparePartList.Add(sparePart);
            }

            List<DataObject> updateDataObjects = new List<DataObject>();
            updateDataObjects.AddRange(brandList);
            updateDataObjects.AddRange(producingCountryList);
            updateDataObjects.AddRange(carList);
            updateDataObjects.AddRange(sparePartList);

            DataObject[] updateObjectsArray = updateDataObjects.ToArray();
            dataService.UpdateObjects(ref updateObjectsArray);

            return true;
        }
    }
}
