# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TZ_Infotecs_Winter_2026.Api/TZ_Infotecs_Winter_2026.Api.csproj", "TZ_Infotecs_Winter_2026.Api/"]
COPY ["TZ_Infotecs_Winter_2026.Application/TZ_Infotecs_Winter_2026.Application.csproj", "TZ_Infotecs_Winter_2026.Application/"]
COPY ["TZ_Infotecs_Winter_2026.Infrastructure/TZ_Infotecs_Winter_2026.Infrastructure.csproj", "TZ_Infotecs_Winter_2026.Infrastructure/"]
COPY ["TZ_Infotecs_Winter_2026.Domain/TZ_Infotecs_Winter_2026.Domain.csproj", "TZ_Infotecs_Winter_2026.Domain/"]
RUN dotnet restore "./TZ_Infotecs_Winter_2026.Api/TZ_Infotecs_Winter_2026.Api.csproj"
COPY . .
WORKDIR "/src/TZ_Infotecs_Winter_2026.Api"
RUN dotnet build "./TZ_Infotecs_Winter_2026.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TZ_Infotecs_Winter_2026.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TZ_Infotecs_Winter_2026.Api.dll"]