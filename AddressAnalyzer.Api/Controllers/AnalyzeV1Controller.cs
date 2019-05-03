using AddressAnalyzer.Common.DataContracts;
using Microsoft.AspNetCore.Mvc;
using AddressAnalyzer.Common.Extensions;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using AddressAnalyzer.Common.Workers;
using System;

namespace AddressAnalyzer.Api.Controllers
{
    [ControllerName("Analyze")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AnalyzeV1Controller : Controller
    {
        public AnalyzeV1Controller(IConfiguration config)
        {
            _config = config;
        }

        private IConfiguration _config { get; }

        [HttpGet("{address}")]
        public async Task<AnalysisResult> Get(string address)
        {
            this.ValidateAddressInput(address);


            // TODO: Implement
            return new AnalysisResult
            {
                GeoIp = await GetGeoIpDataAsync(address)
            };
        }

        private async Task<GeoIpAnalysisResult> GetGeoIpDataAsync(string address)
        {
            string geoIpApiUrl = _config.GetValue<string>("geoIpApiUrl");
            Uri geoIpAnalyzeRoute = new Uri($"{geoIpApiUrl}/api/v1/analyze/{address}");

            GeoIpWorker geoIpWorker = new GeoIpWorker(geoIpAnalyzeRoute);
            GeoIpAnalysisResult result = await geoIpWorker.FetchDataAsync();

            return result;
        }
    }
}
