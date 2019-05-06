using System;
using System.Threading.Tasks;
using AddressAnalyzer.Common.DataContracts;
using AddressAnalyzer.ReverseDns.Api.Providers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AddressAnalyzer.ReverseDns.Api.Tests
{
    [TestFixture]
    public class ReverseDnsClientTests
    {
        [Test]
        public async Task ReverseDnsClientReturnsJsonWhenPresentedValidIpAddress()
        {
            ReverseDnsClient<MxToolboxReverseDnsDataProvider> client = new ReverseDnsClient<MxToolboxReverseDnsDataProvider>();

            string result = await client.PostDataAsync(new MxToolboxPayload
            {
                inputText = $"ptr:8.8.8.8",
                resultIndex = 1
            });

            try
            {
                _ = JsonConvert.DeserializeObject(result);
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task ReverseDnsClientReturnsEmptyStringWhenPresentedWithInvalidAddress()
        {
            ReverseDnsClient<MxToolboxReverseDnsDataProvider> client = new ReverseDnsClient<MxToolboxReverseDnsDataProvider>();

            string result = await client.GetDataAsync("!@#$$%");

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public async Task ReverseDnsClientReturnsJsonWhenPresentedWithValidDomain()
        {
            ReverseDnsClient<MxToolboxReverseDnsDataProvider> client = new ReverseDnsClient<MxToolboxReverseDnsDataProvider>();

            string result = await client.PostDataAsync(new MxToolboxPayload
            {
                inputText = $"ptr:google.com",
                resultIndex = 1
            });

            try
            {
                _ = JsonConvert.DeserializeObject(result);
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
