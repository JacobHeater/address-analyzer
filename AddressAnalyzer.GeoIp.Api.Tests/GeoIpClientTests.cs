using System.Threading.Tasks;
using AddressAnalyzer.Common;
using AddressAnalyzer.GeoIp.Api.Providers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AddressAnalyzer.GeoIp.Api.Tests
{
    [TestFixture]
    public class GeoIpClientTests
    {
        [Test]
        public async Task GeoIpClinetReturnsDataAsJsonWhenJsonProviderRequested()
        {
            GeoIpClient<IPApiJsonDataProvider> client = new GeoIpClient<IPApiJsonDataProvider>();
            string data = await client.GetDataAsync("8.8.8.8/json");

            Assert.IsFalse(data.Contains(Constants.ERROR_PREFIX));

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