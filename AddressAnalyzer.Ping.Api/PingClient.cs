using System;
using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.Ping.Api
{
    public class PingClient<TDataProvider> : DataClientBase<TDataProvider> where TDataProvider : class, IDataProvider, new()
    {

    }
}
