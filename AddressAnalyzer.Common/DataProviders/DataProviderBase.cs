using System;
using System.Collections.Generic;
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

        public Uri GetUrl()
        {
            return new Uri(ProviderUrl);
        }

        public Task<string> GetResultAsync(Uri uri)
        {
            return RestClient.GetAsync(uri);
        }

        public Task<string> GetResultAsync(Uri uri, Dictionary<string, string> parameters)
        {
            return RestClient.GetAsync(uri, parameters);
        }

        public Task<string> PostDataAsync<TData>(Uri uri, TData payload) where TData : class, new()
        {
            return RestClient.PostAsync(uri, payload);
        }
    }
}
