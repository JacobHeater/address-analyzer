using System;
using System.Threading.Tasks;

namespace AddressAnalyzer.Common.DataProviders
{
    public abstract class DataClient<TDataProvider> where TDataProvider : class, IDataProvider, new()
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
            string json = await Provider.GetResultAsync(uri);

            return json;
        }
    }
}
