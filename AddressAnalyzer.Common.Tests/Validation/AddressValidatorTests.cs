using System.Net;
using AddressAnalyzer.Common.Validation;
using NUnit.Framework;

namespace AddressAnalyzer.Common.Tests.Validation
{
    [TestFixture]
    public class AddressValidatorTests
    {
        [Test]
        public void AddressValidatorReturnsFalseForEmptyString()
        {
            string input = string.Empty;
            bool expected = false;
            bool actual = AddressValidator.IsAddressValid(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddressValidatorReturnsTrueForIpV4Address()
        {
            string input = IPAddress.Loopback.ToString();
            bool expected = true;
            bool actual = AddressValidator.IsAddressValid(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddressValidatorReturnsFalseForInvalidIpV4Address()
        {
            string input = "123";
            bool expected = false;
            bool actual = AddressValidator.IsAddressValid(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddressValidatorReturnsTrueForIpV6Address()
        {
            string input = IPAddress.IPv6Loopback.ToString();
            bool expected = true;
            bool actual = AddressValidator.IsAddressValid(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddressValidatorReturnsTrueForValidUrl()
        {
            string input = "https://www.google.com";
            bool expected = true;
            bool actual = AddressValidator.IsAddressValid(input);

            Assert.AreEqual(expected, actual);

            input = "https://google.com";
            expected = true;
            actual = AddressValidator.IsAddressValid(input);

            Assert.AreEqual(expected, actual);

            input = "google.com";
            expected = true;
            actual = AddressValidator.IsAddressValid(input);

            Assert.AreEqual(expected, actual);

            input = "www.google.com";
            expected = true;
            actual = AddressValidator.IsAddressValid(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
