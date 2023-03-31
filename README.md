# Flexberry.NiFiSample

Приложение для примера работы с Apache NiFi. Состоит из приложения стенда, postgres базы данных приложения, clickhouse баз данных аудита и аналитической таблицы, а также сервисов Grafana Loki, NiFi, Superset. Все это развернуто в докере.
## Необходимые для запуска примера технологии
Для запуска примера потребуется:
1. [Docker](https://docs.docker.com/desktop/install/windows-install/)

## Последовательность действий для запуска

1. Собрать Docker-образы

```
\src\Docker> .\create-image.cmd
```

2. Запустить Docker-образы

```
\src\Docker> .\start.cmd
```

3. В запущенном из образа nifisample/audit-clickhouse контейнере (например, с помощью плагина Doker в VS Code - нажать правой кнопкой мыши по контейнеру -> Attach Shell) выполнить команду для создания таблицы

```
clickhouse-client --host audit-clickhouse-db --user default --password P@ssw0rd --multiquery < /var/clickhouse/schema/ClickhouseAuditCreate.sql
```

Для nifisample/clickhouseanalytics:

```
clickhouse-client --host clickhouse-analytics-db --user default --password P@ssw0rd --multiquery < /var/clickhouse/schema/ClickhouseAnalytics.create.sql
```

Теперь все запущено

* <http://localhost> - web приложение
* <https://localhost:8443/> - nifi (логин: flexberryuser пароль: jhvjhvjhvjhv)
* <http://localhost:5432/> - postgres бд приложения (логин: flexberryuser пароль: jhv)
* <http://localhost:8123/> - clickhouse бд аудита (логин: default пароль: P@ssw0rd)
* <http://localhost:3000/> - grafana (логин: admin пароль: usr123)
* <http://localhost:8124/> - clickhouse бд аналитическая (логин: default пароль: P@ssw0rd)
* <http://localhost:8088/> - superset

4. Остановить выполнение Docker-образов

```
\src\Docker> .\stop.cmd
```

## Работа с nifi

Необходимые для создания подключений к БД драйверы jdbc находятся в каталоге /opt/jdbc

* postgresql-42.5.4.jar (класс org.postgresql.Driver, url соединения: jdbc:postgresql://app-postgre-db:5432/appdb) - драйвер для PostgreSQL
* clickhouse-jdbc-0.4.1-shaded.jar (класс com.clickhouse.jdbc.ClickHouseDriver, url соединения: jdbc:ch:https://clickhouse-db:8123/default?ssl=false) - драйвер ClickHouse
