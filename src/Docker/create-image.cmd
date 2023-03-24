docker build --no-cache -f Dockerfiles/Dockerfile -t nifisample/app ../..

docker build --no-cache -f Dockerfiles/Dockerfile.PostgreSql -t nifisample/postgre-sql ../SQL

docker build --no-cache -f Dockerfiles/Dockerfile.Audit.Clickhouse -t nifisample/audit-clickhouse ../SQL

docker build --no-cache -f Dockerfile.NiFi -t nifisample/nifi ..
