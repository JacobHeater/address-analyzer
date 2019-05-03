using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.VirusTotal.Api
{
    public class VirusTotalClient<TDataProvider> : DataClientBase<TDataProvider> where TDataProvider : class, IDataProvider, new()
    {

    }
}
