using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.GeoIp.Api
{
    public class GeoIpClient<TDataProvider> : DataClientBase<TDataProvider> where TDataProvider : class, IDataProvider, new()
    {
    }
}
