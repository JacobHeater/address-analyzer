using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.GeoIp.Api.Providers
{
    public class IPApiJsonDataProvider : DataProviderBase
    {
        public override string ProviderUrl => "http://ip-api.com/json";
    }
}
