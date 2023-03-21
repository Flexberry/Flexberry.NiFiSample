



CREATE TABLE "CarData" (

 "primaryKey" UUID,

 "BrandName" String,

 "CarNumber" String,

 "CarBody" String,

 "CarDate" DateTime,

 "SparePartName" String,

 "SparePartQuantity" Int32,

 "SparePartUsed" UInt8,

 "SparePartProducingCountryName" String

) ENGINE = MergeTree() ORDER BY ("primaryKey");

CREATE TABLE "CarDataBuffer" as "CarData" ENGINE = Buffer(currentDatabase(), "CarData", 16, 0.1, 2, 10, 10000, 100000, 1000000);




