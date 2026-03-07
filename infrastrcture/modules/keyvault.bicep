@description('Prefix for resources')
param prefix string
param location string
param mysqlUser string
@secure() 
param mysqlPassword string

var kvName = '${prefix}-kv-${substring(uniqueString(resourceGroup().id), length(uniqueString(resourceGroup().id)) - 6, 6)}'

resource keyVault 'Microsoft.KeyVault/vaults@2023-02-01' = {
  name: kvName
  location: location
  properties: {
    sku: { family: 'A', name: 'standard' }
    tenantId: subscription().tenantId
    enableSoftDelete: true
    enableRbacAuthorization: true
    enabledForTemplateDeployment: true
    accessPolicies: []
  }
}

resource kvUser 'Microsoft.KeyVault/vaults/secrets@2023-02-01' = {
  parent: keyVault
  name: 'MYSQL_USER'
  properties: { value: mysqlUser }
}

resource kvPassword 'Microsoft.KeyVault/vaults/secrets@2023-02-01' = {
  parent: keyVault
  name: 'MYSQL_PASSWORD'
  properties: { value: mysqlPassword }
}

output keyVaultId string = keyVault.id
output kvUserSecretUri string = kvUser.properties.secretUriWithVersion
output kvPasswordSecretUri string = kvPassword.properties.secretUriWithVersion