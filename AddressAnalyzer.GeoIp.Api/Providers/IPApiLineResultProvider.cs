using System;
using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.GeoIp.Api.Providers
{
    public class IPApiLineResultProvider : DataProviderBase
    {
        public override string ProviderUrl => "http://ip-api.com/line";
    }
}
