using System;
using System.Net;

namespace AddressAnalyzer.Common.Validation
{
    /// <summary>
    /// A utility class for validating addresses, such as URLs, hyperlinks,
    /// or IP addresses.
    /// </summary>
    public static class AddressValidator
    {
        /// <summary>
        /// Returns a boolean indicating if the addressis valid
        /// or not.
        /// </summary>
        /// <returns><c>true</c>, if address valid was valid, <c>false</c> otherwise.</returns>
        /// <param name="address">Address to validate.</param>
        public static bool IsAddressValid(string address)
        {
            return GetAddressType(address) != AddressType.Unknown;
        }

        public static AddressType GetAddressType(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return AddressType.Unknown;
            }

            AddressType addressType = AddressType.Unknown;

            // Try URI validation first.
            try
            {
                _ = new Uri(address);
                addressType = AddressType.Domain;
            }
            catch (Exception)
            {
                addressType = AddressType.Unknown;
            }

            if (addressType == AddressType.Unknown)
            {
                try
                {
                    Uri uri = new Uri($"http://{address}");

                    if (uri.HostNameType == UriHostNameType.IPv4 || uri.HostNameType == UriHostNameType.IPv6)
                    {
                        if (uri.ToString().ToLower() != address.ToLower())
                        {
                            addressType = AddressType.Unknown;
                        }
                    }
                    else
                    {
                        addressType = AddressType.Domain;
                    }
                }
                catch (Exception)
                {
                    addressType = AddressType.Unknown;
                }
            }

            if (addressType == AddressType.Unknown)
            {
                // Try IP v4/v6 validation
                try
                {
                    // It's possible that IPAddress.Parse
                    // will recoginize "123" as "0.0.0.123"
                    // which we don't want. Therefore, to make
                    // sure what was parsed is the same as what
                    // was presented, we'll check for equality.

                    IPAddress addr = IPAddress.Parse(address);

                    if (addr.ToString().ToLower() == address.ToLower())
                    {
                        addressType = AddressType.IPAddress;
                    }
                    else
                    {
                        addressType = AddressType.Unknown;
                    }
                }
                catch (Exception)
                {
                    addressType = AddressType.Unknown;
                }
            }

            return addressType;
        }
    }
}
