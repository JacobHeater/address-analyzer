using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressAnalyzer.Common.Tests.DataProviders
{
    [TestFixture]
    public class DataClientBaseTests
    {
        [Test]
        public async Task DataClientBaseThrowsArgumentExceptionWhenAddressIsEmpty()
        {
            MockDataClient client = new MockDataClient();

            try
            {
                _ = await client.GetDataAsync(string.Empty);

                Assert.Fail("We should not have got here...");
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task DataClientBaseThrowsArgumentNullExceptionWhenParametersAreNull()
        {
            MockDataClient client = new MockDataClient();

            try
            {
                _ = await client.GetDataAsync(default(Dictionary<string, string>));

                Assert.Fail("We should not have got here...");
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task DataClientBaseThrowsArgumentNullExceptionWhenPayloadIsNull()
        {
            MockDataClient client = new MockDataClient();

            try
            {
                _ = await client.PostDataAsync<object>(null);

                Assert.Fail("We should not have got here...");
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }
        }
    }
}
