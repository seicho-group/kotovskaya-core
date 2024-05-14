FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR ./
COPY ["Kotovskaya.Categories/Kotovskaya.Categories.csproj", "Kotovskaya.Categories/"]
COPY ["Kotovskaya.Shared/Kotovskaya.Shared.csproj", "Kotovskaya.Shared/"]
COPY ["Kotovskaya.DB/Kotovskaya.DB.csproj", "Kotovskaya.DB/"]
COPY ["Kotovskaya.TelegramApi/Kotovskaya.TelegramApi.csproj", "Kotovskaya.TelegramApi/"]
COPY ["Kotovskaya.Products/Kotovskaya.Products.csproj", "Kotovskaya.Products/"]
RUN dotnet restore "Kotovskaya.Categories/Kotovskaya.Categories.csproj"
COPY . .
WORKDIR "/src/Kotovskaya.Categories"
RUN dotnet build "Kotovskaya.Categories.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Kotovskaya.Categories.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kotovskaya.Categories.dll"]
