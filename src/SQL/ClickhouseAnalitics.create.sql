CREATE TABLE Analitics
(
    "primaryKey" UUID,
    "OperationTime" DateTime,
    "OperationId" Nullable(UUID),
    "OperationTags" String,
    "OperationType" String,
    "ObjectType" Nullable(String),
    "UserName" Nullable(String),
    "UserLogin" Nullable(String),
    "CarData" Nullable(String)
)
ENGINE = MergeTree()
ORDER BY OperationTime
SETTINGS index_granularity = 8192;


CREATE TABLE AnaliticsBuffer AS Analitics ENGINE = Buffer(currentDatabase(), Analitics, 16, 0.1, 2, 10, 10000, 100000, 1000000);
