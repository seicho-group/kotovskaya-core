# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
ENV ASPNETCORE_HTTP_PORTS 8003
EXPOSE 8003

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY . ./

RUN dotnet publish ./Kotovskaya.Order.csproj -c Release -o out --no-self-contained

FROM mcr.microsoft.com/dotnet/sdk:8.0

COPY --from=build /out .

ENTRYPOINT [ "dotnet", "/Kotovskaya.Order.dll" ]
