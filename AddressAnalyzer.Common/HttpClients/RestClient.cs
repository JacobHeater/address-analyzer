using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using Client = RestSharp.RestClient;

namespace AddressAnalyzer.Common.HttpClients
{
    public class RestClient
    {
        private readonly Client _restClient = new Client();

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

        private bool IsResponseValid(IRestResponse response)
        {
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
