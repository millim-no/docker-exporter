FROM mcr.microsoft.com/dotnet/sdk:6.0.420-alpine3.18 as build

COPY src /src
WORKDIR /src
#RUN dotnet publish -c Release -o /build
ARG TARGETPLATFORM
RUN chmod +x build.sh
RUN ./build.sh

FROM alpine:3.18

RUN apk add ca-certificates-bundle libgcc libssl3 libstdc++ zlib libgdiplus icu-libs
#FROM mcr.microsoft.com/dotnet/runtime:6.0-alpine3.18

COPY --from=build /build /build
RUN chmod +x /build/docker-exporter
ENTRYPOINT ["/build/docker-exporter", "--urls", "http://0.0.0.0:18085"]

EXPOSE 18085
