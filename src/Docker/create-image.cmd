docker build --no-cache -f Dockerfiles/Dockerfile -t nifisample/app ../..

docker build --no-cache -f Dockerfiles/Dockerfile.PostgreSql -t nifisample/postgre-sql ../SQL

docker build --no-cache -f Dockerfiles/Dockerfile.Clickhouse -t nifisample/clickhouse ../SQL

