@description('Prefix for resources')
param prefix string
param location string

resource containerRegistry 'Microsoft.ContainerRegistry/registries@2022-12-01' = {
  name: '${prefix}acr'
  location: location
  sku: { name: 'Basic' }
  properties: { adminUserEnabled: true }
}