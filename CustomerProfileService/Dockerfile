FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CustomerProfileService/CustomerProfileService.csproj", "CustomerProfileService/"]
COPY ["CustomerOperations/CustomerOperations.csproj", "CustomerOperations/"]
COPY ["SharedCore/SharedOperations.csproj", "SharedCore/"]
RUN dotnet restore "./CustomerProfileService/CustomerProfileService.csproj"
COPY . .
WORKDIR "/src/CustomerProfileService"
RUN dotnet build "./CustomerProfileService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CustomerProfileService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerProfileService.dll"]