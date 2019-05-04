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
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Argument 'path' is required to have a value.", nameof(path));
            }

            if (string.IsNullOrWhiteSpace(ProviderUrl))
            {
                throw new EmptyProviderUrlException();
            }

            return new Uri($"{ProviderUrl}/{path}");
        }

        public Uri GetUrl()
        {
            if (string.IsNullOrWhiteSpace(ProviderUrl))
            {
                throw new EmptyProviderUrlException();
            }

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
