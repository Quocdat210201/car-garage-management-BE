FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/Gara.Management/Gara.Management.Api/Gara.Management.Api.csproj", "src/services/Gara.Management/Gara.Management.Api/"]
COPY ["src/infrastructures/Gara.Cache.Redis/Gara.Cache.Redis.csproj", "src/infrastructures/Gara.Cache.Redis/"]
COPY ["src/infrastructures/QuotePlatform.Cache/Gara.Cache.csproj", "src/infrastructures/QuotePlatform.Cache/"]
COPY ["src/infrastructures/Gara.Exceptions/Gara.Exceptions.csproj", "src/infrastructures/Gara.Exceptions/"]
COPY ["src/infrastructures/Gara.Domain/Gara.Domain.csproj", "src/infrastructures/Gara.Domain/"]
COPY ["src/infrastructures/Gara.Persistance.Ef/Gara.Persistance.Ef.csproj", "src/infrastructures/Gara.Persistance.Ef/"]
COPY ["src/framework/Gara.Persistance/Gara.Persistance.csproj", "src/framework/Gara.Persistance/"]
COPY ["src/services/Gara.Management/Gara.Management.Application/Gara.Management.Application.csproj", "src/services/Gara.Management/Gara.Management.Application/"]
COPY ["src/services/Gara.Management/Gara.Management.Domain/Gara.Management.Domain.csproj", "src/services/Gara.Management/Gara.Management.Domain/"]
COPY ["Gara.Extensions/Gara.Extensions.csproj", "Gara.Extensions/"]
COPY ["src/services/Gara.Identity/Gara.Identity.Domain/Gara.Identity.Domain.csproj", "src/services/Gara.Identity/Gara.Identity.Domain/"]
RUN dotnet restore "./src/services/Gara.Management/Gara.Management.Api/./Gara.Management.Api.csproj"
COPY . .
WORKDIR "/src/src/services/Gara.Management/Gara.Management.Api"
RUN dotnet build "./Gara.Management.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Gara.Management.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gara.Management.Api.dll"]