using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.Rdap.Api.Providers
{
    public class OpenRdapIpAddressJsonDataProvider : DataProviderBase
    {
        public override string ProviderUrl => "https://www.rdap.net/ip";
    }
}
