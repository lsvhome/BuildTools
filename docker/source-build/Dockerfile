FROM dotnet-source-build:fedora-23-compiled

ENV \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true \
    DOTNET_HOME=/source-build/Tools/dotnetcli \
    KOREBUILD_DOTNET_VERSION=latest \
    KOREBUILD_SKIP_RUNTIME_INSTALL=true

# Prevent install of NetFx reference assembies
RUN mkdir -p ${DOTNET_HOME}/buildtools/netfx/4.6.1

WORKDIR /buildtools

COPY . .

# Restore in separate layer to reduce incremental build times when
# restore succeeds but compile fails
RUN ./build.sh -v /t:Restore /p:Configuration=Release /p:ExcludeTests=true

RUN ./build.sh -v /t:Package /p:Configuration=Release /p:ExcludeTests=true
