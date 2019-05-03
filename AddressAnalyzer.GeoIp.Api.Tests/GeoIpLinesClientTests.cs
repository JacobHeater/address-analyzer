using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AddressAnalyzer.GeoIp.Api.Tests
{
    [TestFixture]
    public class GeoIpLinesClientTests
    {
        [Test]
        public async Task GeoIpLinesClientReturnsDataWithNewLines()
        {
            GeoIpLinesClient client = new GeoIpLinesClient();
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
    }
}