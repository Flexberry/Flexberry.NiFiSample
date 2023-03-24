docker build --no-cache -f SQL\Dockerfile.PostgreSql -t nifisample/postgre-sql ../SQL

docker build --no-cache -f Dockerfile -t nifisample/app ../..

docker build --no-cache -f Dockerfile.NiFi -t nifisample/nifi ..
