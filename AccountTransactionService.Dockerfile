FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AccountTransactionService/AccountTransactionService.csproj", "AccountTransactionService/"]
RUN dotnet restore "AccountTransactionService/AccountTransactionService.csproj"
COPY . .
WORKDIR "/src/AccountTransactionService"
RUN dotnet build "AccountTransactionService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccountTransactionService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountTransactionService.dll"]
