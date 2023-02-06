FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StoreManagement.Api/StoreManagement.Api.csproj", "StoreManagement.Api/"]
RUN dotnet restore "StoreManagement.Api\StoreManagement.Api.csproj"
COPY . .
WORKDIR "/src/StoreManagement.Api"
RUN dotnet build "StoreManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StoreManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StoreManagement.Api.dll"]
