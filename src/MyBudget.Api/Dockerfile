FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/MyBudget.Api/MyBudget.Api.csproj", "src/MyBudget.Api/"]
RUN dotnet restore "src/MyBudget.Api/MyBudget.Api.csproj"
COPY . .
WORKDIR "/src/src/MyBudget.Api"
RUN dotnet build "MyBudget.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MyBudget.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MyBudget.Api.dll"]