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

        [Test]
        public void AddressValidatorReturnsIpAddressTypeWhenStringIsIpAddress()
        {
            string input = "8.8.8.8";
            AddressType expected = AddressType.IPAddress;
            AddressType actual = AddressValidator.GetAddressType(input);

            Assert.AreEqual(expected, actual);

            input = IPAddress.IPv6Loopback.ToString();
            actual = AddressValidator.GetAddressType(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddressValidatorReturnsDomainWhenStringIsDomain()
        {
            string input = "google.com";
            AddressType expected = AddressType.Domain;
            AddressType actual = AddressValidator.GetAddressType(input);

            Assert.AreEqual(expected, actual);

            input = "http://google.com";
            actual = AddressValidator.GetAddressType(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddressValidatorReturnsUnknownWhenInputStringIsInvalid()
        {
            string input = "!@#$%!@#$";
            AddressType expected = AddressType.Unknown;
            AddressType actual = AddressValidator.GetAddressType(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
