using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressAnalyzer.Common.DataProviders
{
    public abstract class DataClientBase<TDataProvider> : IDataClient where TDataProvider : class, IDataProvider, new()
    {
        private TDataProvider _provider;
        private TDataProvider Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = (TDataProvider)Activator.CreateInstance(typeof(TDataProvider));
                }

                return _provider;
            }
        }

        public async Task<string> GetDataAsync(string address)
        {
            Uri uri = Provider.GetUrl(address);
            string result = await Provider.GetResultAsync(uri);

            return result;
        }

        public async Task<string> GetDataAsync(Dictionary<string, string> parameters)
        {
            Uri uri = Provider.GetUrl();
            string result = await Provider.GetResultAsync(uri, parameters);

            return result;
        }

        public async Task<string> PostDataAsync<TData>(TData payload) where TData : class, new()
        {
            Uri uri = Provider.GetUrl();
            string result = await Provider.PostDataAsync(uri, payload);

            return result;
        }
    }
}
