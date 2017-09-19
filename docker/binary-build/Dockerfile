FROM microsoft/dotnet-nightly:2.1.0-preview1-runtime-deps-jessie

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
# buildtools dependencies
        curl \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /buildtools

ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE true

COPY . .
RUN ./build.sh -v /t:Package
