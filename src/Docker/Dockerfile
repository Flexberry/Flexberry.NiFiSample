FROM node:12 as ember_app

COPY /src/ember-app /src/ember-app

RUN \
  set -x; \
  cd /src/ember-app/; \
  yarn global add ember-cli@3.1.4; \
  yarn global add bower;

RUN \
  set -x; \
  cd /src/ember-app/; \
  yarn install; \
  bower install --allow-root;

RUN \
  set -x; \
  cd /src/ember-app/; \
  ember build --environment=docker

FROM mono:latest as backend

COPY /src/NiFiSample /src/NiFiSample

RUN \
 cd /src/NiFiSample; \
 nuget restore NiFiSample.sln; \
 msbuild /p:Configuration=Release NiFiSample.sln

FROM flexberry/mono-xsp:latest

MAINTAINER mail@flexberry.net

COPY --from=backend /src/NiFiSample/ODataBackend /app
COPY --from=ember_app /src/ember-app/dist /app
WORKDIR /app
