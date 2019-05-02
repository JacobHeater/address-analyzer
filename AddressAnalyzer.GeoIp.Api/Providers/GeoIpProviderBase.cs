using System;
using System.Threading.Tasks;
using AddressAnalyzer.Common.HttpClients;

namespace AddressAnalyzer.GeoIp.Api.Providers
{
    internal abstract class GeoIpProviderBase
    {
        internal abstract string ProviderUrl { get; }
        protected readonly RestClient RestClient = new RestClient();

        internal string GetUrl(string path)
        {
            return $"{ProviderUrl}/{path}";
        }

        internal Task<string> GetJsonResultAsync(string uri)
        {
            return RestClient.GetAsync(uri);
        }
    }
}
