#!/bin/bash

set -e

ENVIRONMENT=${1:-dev}
RESOURCE_GROUP="skyvault-${ENVIRONMENT}-rg"
LOCATION="eastus"
TEMPLATE_PATH="./infrastructure/bicep/main.bicep"

echo "🚀 Deploying SkyVault Entra ID resources to $ENVIRONMENT environment..."

# Create resource group if it doesn't exist
az group create \
  --name "$RESOURCE_GROUP" \
  --location "$LOCATION"

# Deploy Bicep template
az deployment group create \
  --name "skyvault-entraId-${ENVIRONMENT}-$(date +%s)" \
  --resource-group "$RESOURCE_GROUP" \
  --template-file "$TEMPLATE_PATH" \
  --parameters environment="$ENVIRONMENT"

# Get outputs
echo ""
echo "✅ Deployment complete! Retrieving outputs..."

OUTPUTS=$(az deployment group show \
  --name "skyvault-entraId-${ENVIRONMENT}-$(date +%s | tail -1)" \
  --resource-group "$RESOURCE_GROUP" \
  --query "properties.outputs" -o json)

echo "$OUTPUTS" | jq .

echo ""
echo "📝 Add these values to your .env.local file:"
echo "AZUREAD__CLIENTID=$(echo $OUTPUTS | jq -r '.clientId.value')"
echo "AZUREAD__TENANTID=$(echo $OUTPUTS | jq -r '.tenantId.value')"