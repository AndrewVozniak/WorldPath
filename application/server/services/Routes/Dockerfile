FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 3002

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Routes-Service.csproj", "./"]
RUN dotnet restore "Routes-Service.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Routes-Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Routes-Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

<<<<<<< HEAD
FROM base AS final
=======
ENV ASPNETCORE_URLS=http://*:3002

FROM mcr.microsoft.com/dotnet/aspnet:7.0
>>>>>>> origin/main
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Routes-Service.dll"]
