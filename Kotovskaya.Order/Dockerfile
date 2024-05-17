# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
ENV ASPNETCORE_HTTP_PORTS 8003
EXPOSE 8003

# copy csproj and restore as distinct layers
COPY Kotovskaya.Order/*.csproj Kotovskaya.Order/
COPY Kotovskaya.DB/*.csproj Kotovskaya.DB/
COPY Kotovskaya.Shared/*.csproj Kotovskaya.Shared/
COPY Kotovskaya.TelegramApi/*.csproj Kotovskaya.TelegramApi/
RUN dotnet restore Kotovskaya.Order/Kotovskaya.Order.csproj

# copy and build app and libraries
COPY Kotovskaya.Order/ Kotovskaya.Order/
COPY Kotovskaya.DB/ Kotovskaya.DB/
COPY Kotovskaya.Shared/ Kotovskaya.Shared/
COPY Kotovskaya.TelegramApi/ Kotovskaya.TelegramApi/

FROM build AS publish
WORKDIR /source/Kotovskaya.Order
RUN dotnet publish --no-restore -c Release -o /app --no-self-contained

# final stage/image
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Kotovskaya.Order.dll"]
