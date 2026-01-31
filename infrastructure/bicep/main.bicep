param environment string = 'dev'
param location string = resourceGroup().location
param appName string = 'skyvault'

module entraId './entra-id.bicep' = {
  name: 'entraIdDeployment'
  params: {
    environment: environment
    appName: appName
    replyUrls: [
      'http://localhost:8080/signin-oidc'
      'https://localhost:7199/signin-oidc'
    ]
  }
}

output clientId string = entraId.outputs.clientId
output tenantId string = entraId.outputs.tenantId
output appObjectId string = entraId.outputs.appObjectId
