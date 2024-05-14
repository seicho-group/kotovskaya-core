FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8003

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
RUN ls
COPY ["./Kotovskaya.Order/Kotovskaya.Order.csproj", "Kotovskaya.Order/"]
COPY ["./Kotovskaya.DB/Kotovskaya.DB.csproj", "Kotovskaya.DB/"]
COPY ["./Kotovskaya.Shared/Kotovskaya.Shared.csproj", "Kotovskaya.Shared/"]
COPY ["/src/Kotovskaya.TelegramApi/Kotovskaya.TelegramApi.csproj", "Kotovskaya.TelegramApi/"]
RUN dotnet restore "Kotovskaya.Order/Kotovskaya.Order.csproj"
COPY . .
WORKDIR "/src/Kotovskaya.Order"
RUN dotnet build "Kotovskaya.Order.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Kotovskaya.Order.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kotovskaya.Order.dll"]
