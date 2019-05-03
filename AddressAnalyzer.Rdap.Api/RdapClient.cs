using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.Rdap.Api
{
    public class RdapClient<TDataProvider> : DataClientBase<TDataProvider> where TDataProvider : class, IDataProvider, new()
    {

    }
}
