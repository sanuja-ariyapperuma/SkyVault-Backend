param keyVaultName string
param mysqlUser string

@secure()
param mysqlPassword string

resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' existing = {
  name: keyVaultName
}

resource mysqlUserSecret 'Microsoft.KeyVault/vaults/secrets@2023-02-01' = {
  parent: keyVault
  name: 'mysql-user'
  properties: {
    value: mysqlUser
  }
}

resource mysqlPasswordSecret 'Microsoft.KeyVault/vaults/secrets@2023-02-01' = {
  parent: keyVault
  name: 'mysql-password'
  properties: {
    value: mysqlPassword
  }
}

output mysqlUserSecretUri string = mysqlUserSecret.properties.secretUriWithVersion
output mysqlPasswordSecretUri string = mysqlPasswordSecret.properties.secretUriWithVersion