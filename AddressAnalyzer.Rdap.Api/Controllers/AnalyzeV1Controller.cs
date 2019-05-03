using System.Threading.Tasks;
using AddressAnalyzer.Common.DataContracts;
using Microsoft.AspNetCore.Mvc;
using AddressAnalyzer.Common.Extensions;
using System.Web.Http;
using System.Net;
using AddressAnalyzer.Rdap.Api.Providers;
using AddressAnalyzer.Common.DataProviders;

namespace AddressAnalyzer.Rdap.Api.Controllers
{
    [ControllerName("Analyze")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AnalyzeV1Controller : Controller
    {
        [HttpGet("{type}/{address}")]
        public async Task<RdapAnalysisResult> Get(string type, string address)
        {
            this.ValidateAddressInput(address);

            if (string.IsNullOrEmpty(type))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            IDataClient client;

            switch (type.ToLower())
            {
                case "ip":
                    client = new RdapClient<OpenRdapIpAddressJsonDataProvider>();
                    break;
                case "domain":
                default:
                    client = new RdapClient<OpenRdapDomainJsonDataProvider>();
                    break;
            }

            string data = await client.GetDataAsync(address);
            bool isSuccessful = !string.IsNullOrWhiteSpace(data);

            return new RdapAnalysisResult
            {
                IsSuccessful = isSuccessful,
                ResultText = data
            };
        }
    }
}
