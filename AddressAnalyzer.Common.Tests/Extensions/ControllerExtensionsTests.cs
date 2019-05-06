using System;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using AddressAnalyzer.Common.Extensions;
using System.Web.Http;
using System.Net;

namespace AddressAnalyzer.Common.Tests.Extensions
{
    internal class MockController : Controller
    {
        public void RunTest(string address)
        {
            try
            {
                this.ValidateAddressInput(address);
            }
            catch (HttpResponseException ex)
            {
                Assert.AreEqual(ex.Response.StatusCode, HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }

    [TestFixture]
    public class ControllerExtensionsTests
    {
        [Test]
        public void ValidateAddressInputThrowsHttpResponseExceptionWhenAddressInputIsInvalid()
        {
            MockController ctrl = new MockController();

            ctrl.RunTest("!@#$Q!#@$");
        }

        [Test]
        public void ValidateAddressInputPassesWithValidIpAddress()
        {
            MockController ctrl = new MockController();

            ctrl.RunTest("8.8.8.8");
        }

        [Test]
        public void ValidateAddressInputPassesWithValidDomain()
        {
            MockController ctrl = new MockController();

            ctrl.RunTest("google.com");
        }
    }
}
