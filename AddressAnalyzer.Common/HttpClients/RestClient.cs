using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using Client = RestSharp.RestClient;

namespace AddressAnalyzer.Common.HttpClients
{
    public class RestClient
    {
        private readonly Client _restClient = new Client();

        public async Task<string> GetAsync(string uri)
        {
            try
            {
                IRestResponse response = await _restClient.ExecuteGetTaskAsync(new RestRequest(uri));

                if (IsResponseValid(response))
                {
                    return response.Content;
                }
                else
                {
                    return string.Empty;
                }
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
