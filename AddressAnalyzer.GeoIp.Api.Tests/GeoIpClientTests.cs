using System.Threading.Tasks;
using AddressAnalyzer.GeoIp.Api.Providers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AddressAnalyzer.GeoIp.Api.Tests
{
    [TestFixture]
    public class GeoIpClientTests
    {
        [Test]
        public async Task GeoIpClientReturnsDataWithNewLinesWhenLineProviderRequested()
        {
            GeoIpClient<IPApiLineDataProvider> client = new GeoIpClient<IPApiLineDataProvider>();
            string data = await client.GetDataAsync("google.com");

            Assert.IsFalse(string.IsNullOrWhiteSpace(data));
            Assert.IsTrue(data.Contains("\n"));

            try
            {
                // This should not pass, because
                // what we get back is not JSON.
                _ = JsonConvert.DeserializeObject(data);

                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public async Task GeoIpClinetReturnsDataAsJsonWhenJsonProviderRequested()
        {
            GeoIpClient<IPApiJsonDataProvider> client = new GeoIpClient<IPApiJsonDataProvider>();
            string data = await client.GetDataAsync("google.com");

            Assert.IsFalse(string.IsNullOrWhiteSpace(data));

            try
            {
                _ = JsonConvert.DeserializeObject(data);

                Assert.True(true);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}