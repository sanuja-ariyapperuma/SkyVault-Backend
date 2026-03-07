@description('Prefix for resources')
param prefix string
param location string

resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: '${prefix}-ai'
  location: location
  kind: 'web'
  properties: { Application_Type: 'web' }
}

output instrumentationKey string = appInsights.properties.InstrumentationKey