using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AddressAnalyzer.Common.DataContracts;
using AddressAnalyzer.Common.DataProviders;
using AddressAnalyzer.Common.Extensions;
using AddressAnalyzer.GeoIp.Api.Providers;
using Microsoft.AspNetCore.Mvc;


namespace AddressAnalyzer.GeoIp.Api.Controllers
{
    [ControllerName("Analyze")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AnalyzeV1Controller : Controller
    {
        [HttpGet("{format}/{address}")]
        public async Task<GeoIpAnalysisResult> Get(string format, string address)
        {
            this.ValidateAddressInput(address);

            if (string.IsNullOrWhiteSpace(format))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            IDataClient client;

            switch (format.ToLower())
            {
                case "json":
                    client = new GeoIpClient<IPApiJsonDataProvider>();
                    break;
                case "line":
                default:
                    client = new GeoIpClient<IPApiJsonDataProvider>();
                    break;
            }

            string data = await client.GetDataAsync($"{address}/json");
            bool isSuccessful = !string.IsNullOrWhiteSpace(data);
            
            return new GeoIpAnalysisResult
            {
                IsSuccessful = isSuccessful,
                ResultText = data
            };
        }

        [HttpGet("{address}")]
        public Task<GeoIpAnalysisResult> Get(string address)
        {
            return Get("line", address);
        }
    }
}
