FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY DoelGewicht.Api/DoelGewicht.Api.csproj DoelGewicht.Api/
RUN dotnet restore DoelGewicht.Api/DoelGewicht.Api.csproj

COPY DoelGewicht.Api/ DoelGewicht.Api/
RUN dotnet publish DoelGewicht.Api/DoelGewicht.Api.csproj -c Release -o /app/out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "DoelGewicht.Api.dll"]
