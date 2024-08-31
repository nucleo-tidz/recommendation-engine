#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/nucleotidz.recommendation.engine.schema.manager/nucleotidz.recommendation.engine.schema.manager.csproj", "src/nucleotidz.recommendation.engine.schema.manager/"]
COPY ["src/nucleotidz.recommendation.infrastructure/nucleotidz.recommendation.infrastructure.csproj", "src/nucleotidz.recommendation.infrastructure/"]
RUN dotnet restore "./src/nucleotidz.recommendation.engine.schema.manager/./nucleotidz.recommendation.engine.schema.manager.csproj"
COPY . .
WORKDIR "/src/src/nucleotidz.recommendation.engine.schema.manager"
RUN dotnet build "./nucleotidz.recommendation.engine.schema.manager.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./nucleotidz.recommendation.engine.schema.manager.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "nucleotidz.recommendation.engine.schema.manager.dll"]