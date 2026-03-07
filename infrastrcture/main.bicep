@description('Name prefix for all resources')
param prefix string = 'skyvault'

@description('Location for all resources')
param location string = resourceGroup().location

@description('MySQL appuser username')
param mysqlUser string

@description('MySQL appuser password')
@secure()
param mysqlPassword string

@description('MySQL database name')
param mysqlDatabase string = 'skyvault_db'

@description('Azure AD environment variables')
param azureAdInstance string
param azureAdTenantId string
param azureAdClientId string
param azureAdAudience string

// -------------------------
// Key Vault Module
// -------------------------
module kv 'modules/keyvault.bicep' = {
  name: 'kvDeploy'
  params: {
    prefix: prefix
    location: location
    mysqlUser: mysqlUser
    mysqlPassword: mysqlPassword
  }
}

// -------------------------
// MySQL Module
// -------------------------
module mysql 'modules/mysql.bicep' = {
  name: 'mysqlDeploy'
  params: {
    prefix: prefix
    location: location
    mysqlUser: mysqlUser
    mysqlPassword: mysqlPassword
  }
}

// -------------------------
// Application Insights Module
// -------------------------
module ai 'modules/appInsights.bicep' = {
  name: 'aiDeploy'
  params: {
    prefix: prefix
    location: location
  }
}

// -------------------------
// Container Registry Module
// -------------------------
module acr 'modules/acr.bicep' = {
  name: 'acrDeploy'
  params: {
    prefix: prefix
    location: location
  }
}


// -------------------------
// App Service Module
// -------------------------
module app 'modules/appService.bicep' = {
  name: 'appDeploy'
  params: {
    prefix: prefix
    location: location
    azureAdInstance: azureAdInstance
    azureAdTenantId: azureAdTenantId
    azureAdClientId: azureAdClientId
    azureAdAudience: azureAdAudience
    keyVaultId: kv.outputs.keyVaultId
    kvUserSecretUri: kv.outputs.kvUserSecretUri
    kvPasswordSecretUri: kv.outputs.kvPasswordSecretUri
    mysqlHost: mysql.outputs.mysqlHost
    mysqlDatabase: mysqlDatabase
    appInsightsKey: ai.outputs.instrumentationKey
  }
}

// -------------------------
// 6. MySQL firewall rules (after App Service is deployed)
// -------------------------
module mysqlFirewall 'modules/mysqlFirewall.bicep' = {
  name: 'mysqlFirewallDeploy'
  params: {
    mysqlServerName: mysql.outputs.mysqlServerName
    resourceGroup: resourceGroup().name
    appOutboundIps: app.outputs.outboundIpAddresses
  }
  dependsOn: [app]
}


