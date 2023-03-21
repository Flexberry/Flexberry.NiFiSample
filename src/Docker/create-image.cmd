docker build --no-cache -f SQL\Dockerfile.PostgreSql -t nifisample/postgre-sql ../SQL

docker build --no-cache -f Dockerfile -t nifisample/app ../..
