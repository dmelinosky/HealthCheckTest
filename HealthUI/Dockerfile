#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HealthUI/HealthUI.csproj", "HealthUI/"]
RUN dotnet restore "HealthUI/HealthUI.csproj"
COPY . .
WORKDIR "/src/HealthUI"
RUN dotnet build "HealthUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HealthUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HealthUI.dll"]