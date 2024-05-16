FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.18 as build

COPY src /src
WORKDIR /src
RUN dotnet publish --self-contained -o /build
RUN ls /build

FROM alpine:3.18

RUN apk add ca-certificates-bundle libgcc libssl3 libstdc++ zlib libgdiplus icu-libs

COPY --from=build /build /build
RUN chmod +x /build/docker-exporter
ENTRYPOINT ["/build/docker-exporter", "--urls", "http://0.0.0.0:18085"]

EXPOSE 18085
