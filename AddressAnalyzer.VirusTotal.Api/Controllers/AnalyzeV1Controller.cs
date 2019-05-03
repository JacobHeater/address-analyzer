using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AddressAnalyzer.Common.DataContracts;
using AddressAnalyzer.Common.DataProviders;
using AddressAnalyzer.Common.Extensions;
using AddressAnalyzer.Common.Validation;
using AddressAnalyzer.VirusTotal.Api.Providers;
using Microsoft.AspNetCore.Mvc;

namespace AddressAnalyzer.VirusTotal.Api.Controllers
{
    [ControllerName("Analyze")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AnalyzeV1Controller : Controller
    {
        [HttpGet("{apikey}/{address}")]
        public async Task<VirusTotalAnalysisResult> Get(string apikey, string address)
        {
            this.ValidateAddressInput(address);

            if (string.IsNullOrWhiteSpace(apikey))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            AddressType addressType = AddressValidator.GetAddressType(address);
            IDataClient client;
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "apikey", apikey }
            };

            switch (addressType)
            {
                case AddressType.Domain:
                    client = new VirusTotalClient<VirusTotalDomainReportDataProvider>();
                    parameters.Add("domain", address);
                    break;
                case AddressType.IPAddress:
                    client = new VirusTotalClient<VirusTotalIpAdressReportDataProvider>();
                    parameters.Add("ip", address);
                    break;
                default:
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            string data = await client.GetDataAsync(parameters);
            bool isSuccessful = !string.IsNullOrWhiteSpace(data);

            return new VirusTotalAnalysisResult
            {
                IsSuccessful = isSuccessful,
                ResultText = data
            };
        }
    }
}
