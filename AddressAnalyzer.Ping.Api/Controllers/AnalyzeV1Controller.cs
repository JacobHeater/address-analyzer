using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AddressAnalyzer.Common.DataContracts;
using AddressAnalyzer.Common.Extensions;
using AddressAnalyzer.Ping.Api.Providers;
using Microsoft.AspNetCore.Mvc;

namespace AddressAnalyzer.Ping.Api.Controllers
{
    [ControllerName("Analyze")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AnalyzeV1Controller : Controller
    {
        [HttpGet("{address}")]
        public async Task<PingAnalysisResult> Get(string address)
        {
            this.ValidateAddressInput(address);


            PingClient<HelloAcmPingDataProvider> client = new PingClient<HelloAcmPingDataProvider>();
            string data = await client.GetDataAsync($"?host={address}");

            bool isSuccess = !string.IsNullOrWhiteSpace(data);

            return new PingAnalysisResult
            {
                IsSuccessful = isSuccess,
                ResultText = data
            };
        }
    }
}
