using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.ReverseDns.Api
{
    public class ReverseDnsClient<TDataProvider> : DataClientBase<TDataProvider> where TDataProvider : class, IDataProvider, new()
    {

    }
}
