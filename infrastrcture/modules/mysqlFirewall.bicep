@description('MySQL server name')
param mysqlServerName string

@description('Resource group of MySQL')
param resourceGroup string

@description('Comma-separated App Service outbound IPs')
param appOutboundIps string

var ipList = split(appOutboundIps, ',')

resource mysqlFirewall 'Microsoft.DBforMySQL/flexibleServers/firewallRules@2023-03-01' = [for (ip, i) in ipList: {
  name: '${mysqlServerName}/AllowAppSvcIP${i}'
  properties: { startIpAddress: ip, endIpAddress: ip }
}]