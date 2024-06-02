FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Kotovskaya.MoySkladUpdater/Kotovskaya.MoySkladUpdater.csproj", "Kotovskaya.MoySkladUpdater/"]
RUN dotnet restore "Kotovskaya.MoySkladUpdater/Kotovskaya.MoySkladUpdater.csproj"
COPY . .
WORKDIR "/src/Kotovskaya.MoySkladUpdater"
RUN dotnet build "Kotovskaya.MoySkladUpdater.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Kotovskaya.MoySkladUpdater.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kotovskaya.MoySkladUpdater.dll"]
