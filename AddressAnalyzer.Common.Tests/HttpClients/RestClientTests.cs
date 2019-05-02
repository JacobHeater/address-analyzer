using System;
using System.Threading.Tasks;
using AddressAnalyzer.Common.HttpClients;
using NUnit.Framework;

namespace AddressAnalyzer.Common.Tests.HttpClients
{
    [TestFixture]
    public class RestClientTests
    {
        [Test]
        public async Task RestClientReturnsJsonStringWhenRequestIsValid()
        {
            RestClient client = new RestClient();

            string json = await client.GetAsync("http://ip-api.com/line/google.com");

            Assert.IsFalse(string.IsNullOrWhiteSpace(json));
        }
    }
}
