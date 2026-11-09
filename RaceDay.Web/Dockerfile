FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RaceDay.Web/RaceDay.Web.csproj", "RaceDay.Web/"]
COPY ["RaceDay_API/RaceDay_API.csproj", "RaceDay_API/"] 
RUN dotnet restore "RaceDay.Web/RaceDay.Web.csproj"
COPY . .
WORKDIR "/src/RaceDay.Web"
RUN dotnet build "./RaceDay.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "./RaceDay.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RaceDay.Web.dll"]