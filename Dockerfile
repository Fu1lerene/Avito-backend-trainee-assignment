FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Avito.MerchStore.API/Avito.MerchStore.API.csproj", "Avito.MerchStore.API/"]
COPY ["Avito.MerchStore.Domain/Avito.MerchStore.Domain.csproj", "Avito.MerchStore.Domain/"]
COPY ["Avito.MerchStore.Infrastructure/Avito.MerchStore.Infrastructure.csproj", "Avito.MerchStore.Infrastructure/"]

RUN dotnet restore "Avito.MerchStore.API/Avito.MerchStore.API.csproj"

COPY . .
WORKDIR "/src/Avito.MerchStore.API"

RUN dotnet build "Avito.MerchStore.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Avito.MerchStore.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Avito.MerchStore.API.dll"]