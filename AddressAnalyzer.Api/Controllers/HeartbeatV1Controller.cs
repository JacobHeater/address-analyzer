using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AddressAnalyzer.Api.Controllers
{
    [ControllerName("Heartbeat")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(200)]
    public class HeartbeatV1Controller : Controller
    {
        [HttpGet]
        [SwaggerOperation(Summary = "An endpoint for testing connectivity to the API.", Produces = new string[] { "text/plain" })]
        public string Get()
        {
            return $"Addresss Analyzer Aggregator Api Version 1";
        }
    }
}
