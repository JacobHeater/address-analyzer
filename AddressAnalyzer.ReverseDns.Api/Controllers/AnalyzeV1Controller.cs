using System.Threading.Tasks;
using AddressAnalyzer.Common.DataContracts;
using AddressAnalyzer.Common.Extensions;
using AddressAnalyzer.ReverseDns.Api.Providers;
using Microsoft.AspNetCore.Mvc;

namespace AddressAnalyzer.ReverseDns.Api.Controllers
{
    [ControllerName("Analyze")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AnalyzeV1Controller : Controller
    {
        // GET: api/values
        [HttpGet("{address}")]
        public async Task<ReverseDnsAnalysisResult> Get(string address)
        {
            this.ValidateAddressInput(address);

            ReverseDnsClient<MxToolboxReverseDnsDataProvider> client = new ReverseDnsClient<MxToolboxReverseDnsDataProvider>();

            string data = await client.PostDataAsync(new MxToolboxPayload
            {
                inputText = $"ptr:{address}",
                resultIndex = 1
            });

            bool isSuccess = !string.IsNullOrWhiteSpace(data);

            return new ReverseDnsAnalysisResult
            {
                IsSuccessful = isSuccess,
                ResultText = data
            };
        }
    }
}
