using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using Client = RestSharp.RestClient;

namespace AddressAnalyzer.Common.HttpClients
{
    /// <summary>
    /// A REST client to communicate with RESTful APIs.
    /// </summary>
    public class RestClient
    {
        private readonly Client _restClient = new Client();

        /// <summary>
        /// Makes an HTTP GET request to the given resource.
        /// </summary>
        /// <returns>The string content from the request.</returns>
        /// <param name="uri">The resource to call.</param>
        public async Task<string> GetAsync(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            try
            {
                IRestResponse response = await _restClient.ExecuteGetTaskAsync(new RestRequest(uri.ToString()));

                if (IsResponseValid(response))
                {
                    return response.Content;
                }

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Makes an HTTP GET request with query string parameters
        /// to the given resource.
        /// </summary>
        /// <returns>The string content from the request.</returns>
        /// <param name="uri">The resouce to call.</param>
        /// <param name="parameters">Query string parameters.</param>
        public async Task<string> GetAsync(Uri uri, Dictionary<string, string> parameters)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            try
            {
                RestRequest getRequest = new RestRequest(uri.ToString());

                if (parameters != null && parameters.Count > 0)
                {
                    parameters.ToList().ForEach(kvp => getRequest.AddQueryParameter(kvp.Key, kvp.Value));
                }

                IRestResponse response = await _restClient.ExecuteGetTaskAsync(getRequest);

                if (IsResponseValid(response))
                {
                    return response.Content;
                }

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Makes an HTTP POST request to the given resource,
        /// and posts the payload as JSON.
        /// </summary>
        /// <returns>The data from the request.</returns>
        /// <param name="uri">The resource to query.</param>
        /// <param name="payload">The payload to send.</param>
        /// <typeparam name="TData">The type of the payload.</typeparam>
        public async Task<string> PostAsync<TData>(Uri uri, TData payload) where TData : class, new()
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            try
            {
                RestRequest postRequest = new RestRequest(uri.ToString(), Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };

                postRequest.AddJsonBody(payload);

                IRestResponse response = await _restClient.ExecutePostTaskAsync(postRequest);

                if (IsResponseValid(response))
                {
                    return response.Content;
                }

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Checks to see if the response StatusCode
        /// property is a 200 status code.
        /// </summary>
        /// <returns><c>true</c>, if response code was 200, <c>false</c> otherwise.</returns>
        /// <param name="response">Response.</param>
        private bool IsResponseValid(IRestResponse response)
        {
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
