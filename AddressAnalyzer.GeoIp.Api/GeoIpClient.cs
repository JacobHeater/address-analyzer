using System;
using System.Threading.Tasks;
using AddressAnalyzer.GeoIp.Api.Providers;

namespace AddressAnalyzer.GeoIp.Api
{
    internal class GeoIpClient<TProvider> where TProvider : GeoIpProviderBase
    {
        private GeoIpProviderBase _provider;
        private GeoIpProviderBase Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = (GeoIpProviderBase)Activator.CreateInstance(typeof(TProvider));
                }

                return _provider;
            }
        }

        internal async Task<string> GetGeoIpResultAsync(string address)
        {
            string url = Provider.GetUrl(address);
            string json = await Provider.GetJsonResultAsync(url);

            return json;
        }
    }
}
