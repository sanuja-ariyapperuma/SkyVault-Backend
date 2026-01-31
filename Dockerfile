# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["src/SkyVault.WebApi/SkyVault.WebApi.csproj", "SkyVault.WebApi/"]
COPY ["src/SkyVault/SkyVault.csproj", "SkyVault/"]

# Restore dependencies
RUN dotnet restore "SkyVault.WebApi/SkyVault.WebApi.csproj"

# Copy all source code
COPY src/ .

# Install EF Core CLI in build stage
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Build the project
RUN dotnet build "SkyVault.WebApi/SkyVault.WebApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "SkyVault.WebApi/SkyVault.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage - use aspnet for production
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Install mysql-client for database connectivity checks
RUN apt-get update && \
    apt-get install -y --no-install-recommends default-mysql-client && \
    rm -rf /var/lib/apt/lists/*

# Copy published application
COPY --from=publish /app/publish .

# Copy entrypoint script
COPY docker-entrypoint.sh .
RUN chmod +x docker-entrypoint.sh

# Expose ports
EXPOSE 5000 7199

# Set entry point
ENTRYPOINT ["/bin/bash", "./docker-entrypoint.sh"]
