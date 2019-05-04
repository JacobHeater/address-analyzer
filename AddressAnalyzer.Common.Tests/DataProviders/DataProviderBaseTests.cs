using System;
using AddressAnalyzer.Common.DataProviders;
using NUnit.Framework;

namespace AddressAnalyzer.Common.Tests.DataProviders
{
    [TestFixture]
    public class DataProviderBaseTests
    {
        [Test]
        public void DataProviderBaseThrowsEmptyProviderUrlExceptionWhenProviderGetUrlReturnsEmptyUrl()
        {
            MockErrorDataProvider dataProvider = new MockErrorDataProvider();

            try
            {
                _ = dataProvider.GetUrl();

                Assert.Fail("We should not have made it here...");
            }
            catch (EmptyProviderUrlException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void DataProviderBaseThrowsArgumentExceptionWhenProviderGetUrlDoesNotProvideAValue()
        {
            MockErrorDataProvider dataProvider = new MockErrorDataProvider();

            try
            {
                _ = dataProvider.GetUrl("       ");

                Assert.Fail("We should not have made it here...");
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
        }
    }
}
