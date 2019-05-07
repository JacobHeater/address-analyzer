using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AddressAnalyzer.Common;
using AddressAnalyzer.VirusTotal.Api.Providers;
using NUnit.Framework;

namespace AddressAnalyzer.VirusTotal.Api.Tests
{
    [TestFixture]
    public class VirusTotalClientTests
    {
        [Test]
        public async Task VirusTotalClientReturnsEmptyStringWhenNotPresentedWithValidApiKey()
        {
            VirusTotalClient<VirusTotalDomainReportDataProvider> domainClient = new VirusTotalClient<VirusTotalDomainReportDataProvider>();
            VirusTotalClient<VirusTotalIpAdressReportDataProvider> ipClient = new VirusTotalClient<VirusTotalIpAdressReportDataProvider>();
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "apikey", string.Empty }
            };

            parameters.Add("domain", "google.com");

            string domainReport = await domainClient.GetDataAsync(parameters);

            Assert.True(domainReport.Contains(Constants.ERROR_PREFIX));

            parameters.Remove("domain");
            parameters.Add("ip", "8.8.8.8");

            string ipReport = await ipClient.GetDataAsync(parameters);

            Assert.True(ipReport.Contains(Constants.ERROR_PREFIX));
        }
    }
}
