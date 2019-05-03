using System;
using System.Threading.Tasks;
using AddressAnalyzer.Common.HttpClients;

namespace AddressAnalyzer.Common.DataProviders
{
    public abstract class DataProviderBase : IDataProvider
    {
        public abstract string ProviderUrl { get; }
        protected readonly RestClient RestClient = new RestClient();

        public Uri GetUrl(string path)
        {
            return new Uri($"{ProviderUrl}/{path}");
        }

        public Task<string> GetResultAsync(Uri uri)
        {
            return RestClient.GetAsync(uri);
        }
    }
}
