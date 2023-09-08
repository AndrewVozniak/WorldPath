FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Places-Service/Places-Service.csproj", "Places-Service/"]
RUN dotnet restore "Places-Service/Places-Service.csproj"
COPY . .
WORKDIR "/src/Places-Service"
RUN dotnet build "Places-Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Places-Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Places-Service.dll"]
