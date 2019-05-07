using System;
using System.Threading.Tasks;
using AddressAnalyzer.Common;
using AddressAnalyzer.Rdap.Api.Providers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AddressAnalyzer.Rdap.Api.Tests
{
    [TestFixture]
    public class RdapClientTests
    {
        [Test]
        public async Task RdapClientReturnsJsonForADomainWhenDomainProviderIsSelected()
        {
            RdapClient<OpenRdapDomainJsonDataProvider> client = new RdapClient<OpenRdapDomainJsonDataProvider>();

            string data = await client.GetDataAsync("google.com");

            Assert.False(string.IsNullOrWhiteSpace(data));

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

        [Test]
        public async Task RdapClientReturnsEmptyStringWhenIpAddressIsPresentedToDomainProvider()
        {
            RdapClient<OpenRdapDomainJsonDataProvider> client = new RdapClient<OpenRdapDomainJsonDataProvider>();

            string data = await client.GetDataAsync("8.8.8.8");

            Assert.True(data.Contains(Constants.ERROR_PREFIX));
        }

        [Test]
        public async Task RdapClientReturnsJsonForAnIpAddressWhenIpAddressProviderIsSelected()
        {
            RdapClient<OpenRdapIpAddressJsonDataProvider> client = new RdapClient<OpenRdapIpAddressJsonDataProvider>();

            string data = await client.GetDataAsync("8.8.8.8");

            Assert.False(string.IsNullOrWhiteSpace(data));

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

        [Test]
        public async Task RdapClientReturnsEmptyStringWhenDomainIsPresentedToIpAddressProvider()
        {
            RdapClient<OpenRdapIpAddressJsonDataProvider> client = new RdapClient<OpenRdapIpAddressJsonDataProvider>();

            string data = await client.GetDataAsync("google.com");

            Assert.True(data.Contains(Constants.ERROR_PREFIX));
        }

    }
}
