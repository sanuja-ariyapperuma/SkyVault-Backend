@description('Prefix for resources')
param prefix string
param location string
param mysqlUser string
@secure() 
param mysqlPassword string

resource mysqlServer 'Microsoft.DBforMySQL/flexibleServers@2023-06-30' = {
  name: '${prefix}-mysql'
  location: location
  sku: { name: 'Standard_B1ms', tier: 'Burstable', capacity: 1 }
  properties: {
    administratorLogin: mysqlUser
    administratorLoginPassword: mysqlPassword
    storage: { storageSizeGB: 20 }
    backup: { backupRetentionDays: 7 }
    highAvailability: { mode: 'Disabled' }
    network: { publicNetworkAccess: 'Enabled' }
  }
}

output mysqlHost string = mysqlServer.properties.fullyQualifiedDomainName
output mysqlServerName string = mysqlServer.name