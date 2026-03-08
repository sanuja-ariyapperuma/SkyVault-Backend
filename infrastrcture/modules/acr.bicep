@description('Prefix for resources')
param prefix string
param location string

resource containerRegistry 'Microsoft.ContainerRegistry/registries@2022-12-01' = {
  name: '${prefix}acr${uniqueString(resourceGroup().id)}'
  location: location
  sku: { name: 'Basic' }
  properties: { adminUserEnabled: true }
}

output acrName string = containerRegistry.name
output acrLoginServer string = containerRegistry.properties.loginServer
output acrId string = containerRegistry.id