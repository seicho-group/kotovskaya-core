FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8003
ENV ASPNETCORE_HTTP_PORTS 8003

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Kotovskaya.Order/Kotovskaya.Order.csproj", "Kotovskaya.Order/"]
COPY ["Kotovskaya.DB/Kotovskaya.DB.csproj", "Kotovskaya.DB/"]
COPY ["Kotovskaya.Shared/Kotovskaya.Shared.csproj", "Kotovskaya.Shared/"]
COPY ["Kotovskaya.TelegramApi/Kotovskaya.TelegramApi.csproj", "Kotovskaya.TelegramApi/"]
RUN dotnet publish -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Kotovskaya.Order.dll"]
