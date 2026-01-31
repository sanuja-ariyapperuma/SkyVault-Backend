param environment string
param appName string
param replyUrls array

resource entraIdApp 'Microsoft.AAD/b2cDirectories@2021-04-01' existing = {
  name: tenant().tenantId
}

resource appRegistration 'Microsoft.Graph/applications@v1.0' = {
  displayName: '${appName}-${environment}'
  requiredResourceAccess: [
    {
      resourceAppId: '00000003-0000-0000-c000-000000000000' // Microsoft Graph
      resourceAccess: [
        {
          id: 'e1fe6dd8-ba31-4d61-89e7-88639da4683d'
          type: 'Scope' // User.Read
        }
      ]
    }
  ]
  web: {
    redirectUriSettings: [
      {
        index: 0
        uriValue: replyUrls[0]
      }
      {
        index: 1
        uriValue: replyUrls[1]
      }
    ]
    implicitGrantSettings: {
      enableAccessTokenIssuance: false
      enableIdTokenIssuance: true
    }
  }
}

resource clientSecret 'Microsoft.Graph/applications/addPassword@v1.0' = {
  parent: appRegistration
  body: {
    displayName: 'dev-secret-${uniqueString(resourceGroup().id)}'
    endDateTime: dateTimeAdd(utcNow('u'), 'P1Y')
  }
}

resource servicePrincipal 'Microsoft.Graph/servicePrincipals@v1.0' = {
  displayName: '${appName}-${environment}-sp'
  appId: appRegistration.properties.appId
  accountEnabled: true
}

output clientId string = appRegistration.properties.appId
output tenantId string = subscription().tenantId
output appObjectId string = appRegistration.id
output clientSecret string = clientSecret.properties.secretText
