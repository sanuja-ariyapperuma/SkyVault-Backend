# SkyVault Infrastructure as Code

## Prerequisites

- Azure CLI installed and authenticated (`az login`)
- Bicep CLI (included with recent Azure CLI versions)
- Bash shell

## Deployment

### Deploy to Development

```bash
chmod +x ./infrastructure/scripts/deploy.sh
./infrastructure/scripts/deploy.sh dev
```

This will:
1. Create a resource group for your environment
2. Deploy the Entra ID app registration
3. Create a client secret
4. Output the necessary configuration values

### Manual Setup

If the script fails, you can deploy manually:

```bash
az deployment group create \
  --name skyvault-entraId-dev \
  --resource-group skyvault-dev-rg \
  --template-file ./infrastructure/bicep/main.bicep \
  --parameters environment=dev
```

## Configuration

After deployment, copy your outputs to `.env.local`:

```
AZUREAD__CLIENTID=<from-output>
AZUREAD__TENANTID=<from-output>
AZUREAD__CLIENTSECRET=<from-output>
```

Then update your `docker-compose.yml` to load from `.env.local`.

## Cleanup

To delete all resources:

```bash
az group delete --name skyvault-dev-rg
```