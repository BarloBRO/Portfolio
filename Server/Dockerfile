FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY Server/BarloPortfolio.Server.csproj Server/
RUN dotnet restore Server/BarloPortfolio.Server.csproj
COPY Server/ Server/
RUN dotnet publish Server/BarloPortfolio.Server.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BarloPortfolio.Server.dll"]
