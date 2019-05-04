using System;
using System.Threading.Tasks;
using AddressAnalyzer.Ping.Api.Providers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AddressAnalyzer.Ping.Api.Tests
{
    [TestFixture]
    public class PingClientTests
    {
        [Test]
        public async Task PingClientReturnsJsonWhenHelloAcmProviderIsSelected()
        {
            PingClient<HelloAcmPingDataProvider> client = new PingClient<HelloAcmPingDataProvider>();

            string data = await client.GetDataAsync("?host=8.8.8.8");

            try
            {
                _ = JsonConvert.DeserializeObject(data);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            data = await client.GetDataAsync("?host=google.com");

            try
            {
                _ = JsonConvert.DeserializeObject(data);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

                return;
            }

            Assert.Pass();
        }
    }
}
