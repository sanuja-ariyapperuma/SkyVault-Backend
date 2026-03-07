@description('Prefix for resources')
param prefix string
param location string
param azureAdInstance string
param azureAdTenantId string
param azureAdClientId string
param azureAdAudience string
param keyVaultId string
param kvUserSecretUri string
param kvPasswordSecretUri string
param mysqlHost string
param mysqlDatabase string
param appInsightsKey string

resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: '${prefix}-plan'
  location: location
  sku: { name: 'B1', tier: 'Basic' }
  kind: 'linux'
}

resource appService 'Microsoft.Web/sites@2022-03-01' = {
  name: '${prefix}-api'
  location: location
  kind: 'app,linux,container'
  identity: { type: 'SystemAssigned' }
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|7.0'
      minTlsVersion: '1.2'
      appSettings: [
        { name: 'AZUREAD__INSTANCE', value: azureAdInstance }
        { name: 'AZUREAD__TENANTID', value: azureAdTenantId }
        { name: 'AZUREAD__CLIENTID', value: azureAdClientId }
        { name: 'AZUREAD__AUDIENCE', value: azureAdAudience }
        { name: 'MYSQL_DATABASE', value: mysqlDatabase }
        { name: 'MYSQL_HOST', value: mysqlHost }
        { name: 'MYSQL_PORT', value: '3306' }
        { name: 'MYSQL_USER', value: '@Microsoft.KeyVault(SecretUri=${kvUserSecretUri})' }
        { name: 'MYSQL_PASSWORD', value: '@Microsoft.KeyVault(SecretUri=${kvPasswordSecretUri})' }
        { name: 'APPINSIGHTS_INSTRUMENTATIONKEY', value: appInsightsKey }
      ]
    }
  }
}

// Grant App Service access to Key Vault
resource kvAccessPolicy 'Microsoft.KeyVault/vaults/accessPolicies@2023-02-01' = {
  name: '${keyVaultId}/add'
  properties: {
    accessPolicies: [
      {
        tenantId: subscription().tenantId
        objectId: appService.identity.principalId
        permissions: { secrets: ['get'] }
      }
    ]
  }
  dependsOn: [appService]
}

output outboundIpAddresses string = appService.properties.outboundIpAddresses