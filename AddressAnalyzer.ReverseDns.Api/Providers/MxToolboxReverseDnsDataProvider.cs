using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.ReverseDns.Api.Providers
{
    public class MxToolboxReverseDnsDataProvider : DataProviderBase
    {
        public override string ProviderUrl => "https://mxtoolbox.com/Public/Lookup.aspx/DoLookup2";
    }
}
