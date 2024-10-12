FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CustomerProfileService/CustomerProfileService.csproj", "CustomerProfileService/"]
RUN dotnet restore "CustomerProfileService/CustomerProfileService.csproj"
COPY . .
WORKDIR "/src/CustomerProfileService"
RUN dotnet build "CustomerProfileService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerProfileService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerProfileService.dll"]
