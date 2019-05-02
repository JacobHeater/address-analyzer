using System.Net;
using System.Web.Http;
using AddressAnalyzer.Common.DataContracts;
using AddressAnalyzer.Common.Validation;
using Microsoft.AspNetCore.Mvc;
using AddressAnalyzer.Common.Extensions;

namespace AddressAnalyzer.Api.Controllers
{
    [ControllerName("Analyze")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AnalyzeV1Controller : Controller
    {
        [HttpGet("{address}")]
        public AnalysisResult Get(string address)
        {
            this.ValidateAddressInput(address);

            // TODO: Implement
            return new AnalysisResult();
        }
    }
}
