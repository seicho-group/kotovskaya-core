FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8001
ENV ASPNETCORE_HTTP_PORTS 8001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
RUN dotnet build "kotovskaya-2.0.sln" -c $BUILD_CONFIGURATION -o /app

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "kotovskaya-2.0.sln" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kotovskaya.Categories.dll"]
