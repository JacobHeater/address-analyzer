using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.Common.Tests.DataProviders
{
    public class MockValidDataProvider : DataProviderBase
    {
        public override string ProviderUrl => "https://google.com";
    }
}
