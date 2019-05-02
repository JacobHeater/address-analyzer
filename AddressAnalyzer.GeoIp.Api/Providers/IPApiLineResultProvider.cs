using System;
namespace AddressAnalyzer.GeoIp.Api.Providers
{
    internal class IpApiLineResultProvider : GeoIpProviderBase
    {
        internal override string ProviderUrl => "http://ip-api.com/line";
    }
}
