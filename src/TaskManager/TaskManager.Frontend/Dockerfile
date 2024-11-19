FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TaskManager.Frontend/TaskManager.Frontend.csproj", "TaskManager.Frontend/"]
COPY ["TaskManager.Common/TaskManager.Common.csproj", "TaskManager.Common/"]
RUN dotnet restore "TaskManager.Frontend/TaskManager.Frontend.csproj"
COPY . .
WORKDIR "/src/TaskManager.Frontend"
RUN dotnet build "TaskManager.Frontend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TaskManager.Frontend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskManager.Frontend.dll"]
