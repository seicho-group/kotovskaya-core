FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR ../
COPY ["Kotovskaya.API/Kotovskaya.API.csproj", "Kotovskaya.API/"]
RUN dotnet restore "Kotovskaya.API/Kotovskaya.API.csproj"
COPY . .
WORKDIR "/src/Kotovskaya.API"
RUN dotnet build "Kotovskaya.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Kotovskaya.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN dotnet dev-certs https --trust
ENTRYPOINT ["dotnet", "Kotovskaya.API.dll"]
