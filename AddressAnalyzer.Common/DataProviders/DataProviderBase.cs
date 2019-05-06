using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AddressAnalyzer.Common.HttpClients;

namespace AddressAnalyzer.Common.DataProviders
{
    /// <summary>
    /// This class is responsible for communicating with a service
    /// to provide data for one query. Examples of classes that
    /// can extend this base class are a Ping data provider, or
    /// a Reverse DNS data provider.
    /// </summary>
    public abstract class DataProviderBase : IDataProvider
    {
        /// <summary>
        /// The base URL of the provider.
        /// </summary>
        /// <value>The provider base URL.</value>
        public abstract string ProviderUrl { get; }
        /// <summary>
        /// The REST client that will make the HTTP requests.
        /// </summary>
        protected readonly RestClient RestClient = new RestClient();

        /// <summary>
        /// Returns the base URL with the path concatenated
        /// to the base URL.
        /// </summary>
        /// <returns>The full URL.</returns>
        /// <param name="path">Path to append to the base URL.</param>
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

        /// <summary>
        /// Gets the base URL for the data provider.
        /// </summary>
        /// <returns>The base URL.</returns>
        public Uri GetUrl()
        {
            if (string.IsNullOrWhiteSpace(ProviderUrl))
            {
                throw new EmptyProviderUrlException();
            }

            return new Uri(ProviderUrl);
        }

        /// <summary>
        /// Gets data using the RestClient by making
        /// an HTTP GET request to the given Uri.
        /// </summary>
        /// <returns>The result of the request.</returns>
        /// <param name="uri">Resource to call.</param>
        public Task<string> GetResultAsync(Uri uri)
        {
            return RestClient.GetAsync(uri);
        }

        /// <summary>
        /// Gets data using the RestClient by making an
        /// HTTP GET request to the given Uri, and appends
        /// query string parameters to the Uri.
        /// </summary>
        /// <returns>The result of the request.</returns>
        /// <param name="uri">Resource to call.</param>
        /// <param name="parameters">Query string parameters.</param>
        public Task<string> GetResultAsync(Uri uri, Dictionary<string, string> parameters)
        {
            return RestClient.GetAsync(uri, parameters);
        }

        /// <summary>
        /// Gets data using the RestClient by making an
        /// HTTP POST request to the given Uri, and sends
        /// along the payload.
        /// </summary>
        /// <returns>The data from the request.</returns>
        /// <param name="uri">Resource to call.</param>
        /// <param name="payload">Payload to send with the request.</param>
        /// <typeparam name="TData">The type of the payload.</typeparam>
        public Task<string> PostDataAsync<TData>(Uri uri, TData payload) where TData : class, new()
        {
            return RestClient.PostAsync(uri, payload);
        }
    }
}
