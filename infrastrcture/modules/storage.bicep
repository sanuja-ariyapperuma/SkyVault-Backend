@description('Prefix for resources')
param prefix string

@description('Location for all resources')
param location string

@description('App Service name for network rules')
param appServiceName string

@description('App Service outbound IP addresses for network rules')
param appOutboundIps string

@description('Environment name')
param environment string = 'dev'

@description('Storage account name')
var storageAccountName = '${prefix}${environment}images'

@description('Blob container name for images')
var blobContainerName = 'images'

@description('Parse outbound IP addresses')
var outboundIpArray = split(appOutboundIps, ',')

resource storageAccount 'Microsoft.Storage/storageAccounts@2022-09-01' = {
  name: storageAccountName
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
  properties: {
    accessTier: 'Hot'
    minimumTlsVersion: 'TLS1_2'
    supportsHttpsTrafficOnly: true
    allowBlobPublicAccess: false
    networkAcls: {
      defaultAction: 'Deny'
      bypass: 'AzureServices'
      ipRules: [for ip in outboundIpArray: {
        value: trim(ip)
        action: 'Allow'
      }]
      virtualNetworkRules: []
    }
  }
}

resource blobService 'Microsoft.Storage/storageAccounts/blobServices@2022-09-01' = {
  parent: storageAccount
  name: 'default'
}

resource blobContainer 'Microsoft.Storage/storageAccounts/blobServices/containers@2022-09-01' = {
  parent: blobService
  name: blobContainerName
  properties: {
    publicAccess: 'None'
  }
}

resource appServiceIdentity 'Microsoft.Web/sites@2022-03-01' existing = {
  name: appServiceName
  scope: resourceGroup()
}

resource roleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(storageAccount.id, appServiceIdentity.id, 'StorageBlobDataContributor')
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', 'ba92f5b4-2d11-453d-a403-e96b0029c9fe')
    principalId: appServiceIdentity.identity.principalId
    principalType: 'ServicePrincipal'
  }
  scope: storageAccount
}

output storageAccountName string = storageAccount.name
output blobContainerName string = blobContainerName
output storageAccountId string = storageAccount.id
