using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.Rdap.Api.Providers
{
    public class OpenRdapDomainJsonDataProvider : DataProviderBase
    {
        public override string ProviderUrl => "https://www.rdap.net/domain";
    }
}
