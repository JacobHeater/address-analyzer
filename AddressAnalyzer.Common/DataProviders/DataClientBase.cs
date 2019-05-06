using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressAnalyzer.Common.DataProviders
{
    /// <summary>
    /// Uses the given data provider, as defined in the type
    /// parameter, to retrieve data using the data provider.
    /// </summary>
    /// <typeparam name="TDataProvider">The type of data provider to use.</typeparam>
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

        /// <summary>
        /// Makes an HTTP GET call to the resource passing along
        /// the address to query.
        /// </summary>
        /// <returns>The data from the request.</returns>
        /// <param name="address">Address to query in the data provider.</param>
        public async Task<string> GetDataAsync(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Argument 'address' is required to have a value.", nameof(address));
            }

            Uri uri = Provider.GetUrl(address);
            string result = await Provider.GetResultAsync(uri);

            return result;
        }

        /// <summary>
        /// Makes an HTTP GET call to the resource passing, and
        /// sends along the query string parameters.
        /// </summary>
        /// <returns>The data from the request.</returns>
        /// <param name="parameters">Query string parameters.</param>
        public async Task<string> GetDataAsync(Dictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            Uri uri = Provider.GetUrl();
            string result = await Provider.GetResultAsync(uri, parameters);

            return result;
        }

        /// <summary>
        /// Makes an HTTP POST call to the data provider, and
        /// passes along the payload to the data provider.
        /// </summary>
        /// <returns>The data from the request.</returns>
        /// <param name="payload">Payload to send with the request.</param>
        /// <typeparam name="TData">The type of the payload.</typeparam>
        public async Task<string> PostDataAsync<TData>(TData payload) where TData : class, new()
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            Uri uri = Provider.GetUrl();
            string result = await Provider.PostDataAsync(uri, payload);

            return result;
        }
    }
}
