using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.Ping.Api.Providers
{
    public class HelloAcmPingDataProvider : DataProviderBase
    {
        public override string ProviderUrl => "https://helloacm.com/api/ping";
    }
}
