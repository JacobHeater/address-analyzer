using Microsoft.AspNetCore.Mvc;

namespace AddressAnalyzer.Api.Controllers
{
    [ControllerName("Heartbeat")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HeartbeatV1Controller : Controller
    {
        [HttpGet]
        public string Get()
        {
            return $"Addresss Analyzer Api Version 1";
        }
    }
}
