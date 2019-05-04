using System;
using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.Common.Tests.DataProviders
{
    public class MockErrorDataProvider : DataProviderBase
    {
        public override string ProviderUrl => string.Empty;
    }
}
