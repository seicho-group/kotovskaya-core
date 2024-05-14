FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8004
ENV ASPNETCORE_HTTP_PORTS 8004

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Kotovskaya.Products/Kotovskaya.Products.csproj", "Kotovskaya.Products/"]
COPY ["Kotovskaya.Shared/Kotovskaya.Shared.csproj", "Kotovskaya.Shared/"]
COPY ["Kotovskaya.DB/Kotovskaya.DB.csproj", "Kotovskaya.DB/"]
COPY ["Kotovskaya.TelegramApi/Kotovskaya.TelegramApi.csproj", "Kotovskaya.TelegramApi/"]
RUN dotnet restore "Kotovskaya.Products/Kotovskaya.Products.csproj"
COPY . .
WORKDIR "/src/Kotovskaya.Products"
RUN dotnet build "Kotovskaya.Products.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Kotovskaya.Products.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kotovskaya.Products.dll"]
