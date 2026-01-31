#!/bin/bash

set -e

echo "========== SkyVault API Startup =========="

# Set environment variables to disable HTTPS enforcement
export ASPNETCORE_ENVIRONMENT=Development
export ASPNETCORE_URLS=http://+:5000

# Parse MySQL connection string
DB_HOST=${DB_HOST:-mysql}
DB_PORT=${DB_PORT:-3306}
DB_USER=${MYSQL_USER:-skyvault_user}
DB_PASS=${MYSQL_PASSWORD:-skyvault_pass}
DB_NAME=${MYSQL_DATABASE:-skyvault_dev}

# Wait for database to be ready (using TCP check)
echo "Waiting for MySQL to be ready at ${DB_HOST}:${DB_PORT}..."
for i in {1..30}; do
  if timeout 3 bash -c "cat < /dev/null > /dev/tcp/${DB_HOST}/${DB_PORT}" 2>/dev/null; then
    echo "MySQL TCP connection successful"
    break
  fi
  echo "Attempt $i: MySQL is unavailable - sleeping"
  sleep 2
done

# Wait for MySQL to be fully ready (via health check query)
echo "Waiting for MySQL to be fully operational..."
for i in {1..30}; do
  if mysql -h "${DB_HOST}" -u "${DB_USER}" -p"${DB_PASS}" -e "SELECT 1" > /dev/null 2>&1; then
    echo "MySQL is fully operational"
    break
  fi
  echo "Attempt $i: MySQL query failed - sleeping"
  sleep 2
done

# Verify database exists or create it
echo "Verifying database ${DB_NAME} exists..."
mysql -h "${DB_HOST}" -u "${DB_USER}" -p"${DB_PASS}" -e "CREATE DATABASE IF NOT EXISTS ${DB_NAME};" > /dev/null 2>&1 || true

# Check if database migrations need to be applied
echo "Checking if database migrations are required..."
MIGRATION_CHECK=$(mysql -h "${DB_HOST}" -u "${DB_USER}" -p"${DB_PASS}" "${DB_NAME}" \
  -e "SHOW TABLES LIKE '__EFMigrationsHistory';" 2>/dev/null | grep "__EFMigrationsHistory" || true)

if [ -z "$MIGRATION_CHECK" ]; then
  echo "WARNING: No migration history found. Migrations will be applied on app startup via Entity Framework."
else
  echo "Migration history table found. Migrations may be up-to-date or will be applied on startup."
fi

echo "========== Starting SkyVault.WebApi =========="
echo "Using connection string: Server=${DB_HOST};Port=${DB_PORT};Database=${DB_NAME};User=${DB_USER};"

# Start the application
exec dotnet SkyVault.WebApi.dll
