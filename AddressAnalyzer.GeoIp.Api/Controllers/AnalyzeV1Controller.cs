using System.Threading.Tasks;
using AddressAnalyzer.Common.DataContracts;
using AddressAnalyzer.Common.Extensions;
using AddressAnalyzer.GeoIp.Api.Providers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AddressAnalyzer.GeoIp.Api.Controllers
{
    [ControllerName("Analyze")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AnalyzeV1Controller : Controller
    {
        [HttpGet("{address}")]
        public async Task<GeoIpAnalysisResult> Get(string address)
        {
            this.ValidateAddressInput(address);

            GeoIpClient<IpApiLineResultProvider> client = new GeoIpClient<IpApiLineResultProvider>();

            string lines = await client.GetGeoIpResultAsync(address);
            bool isSuccess = true;

            if (string.IsNullOrWhiteSpace(lines))
            {
                isSuccess = false;
            }

            return new GeoIpAnalysisResult
            {
                IsSuccessful = isSuccess,
                ResultText = lines
            };
        }
    }
}
