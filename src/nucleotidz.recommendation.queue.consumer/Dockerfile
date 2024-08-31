#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/nucleotidz.recommendation.queue.consumer/nucleotidz.recommendation.queue.consumer.csproj", "src/nucleotidz.recommendation.queue.consumer/"]
COPY ["src/nucleotidz.recommendation.infrastructure/nucleotidz.recommendation.infrastructure.csproj", "src/nucleotidz.recommendation.infrastructure/"]
COPY ["src/nucleotidz.recommendation.model/nucleotidz.recommendation.model.csproj", "src/nucleotidz.recommendation.model/"]
RUN dotnet restore "./src/nucleotidz.recommendation.queue.consumer/./nucleotidz.recommendation.queue.consumer.csproj"
COPY . .
WORKDIR "/src/src/nucleotidz.recommendation.queue.consumer"
RUN dotnet build "./nucleotidz.recommendation.queue.consumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./nucleotidz.recommendation.queue.consumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "nucleotidz.recommendation.queue.consumer.dll"]