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
            if (string.IsNullOrWhiteSpace(address))
            {
                return false;
            }

            bool isValid;

            // Try URI validation first.
            try
            {
                _ = new Uri(address);
                isValid = true;
            }
            catch (Exception)
            {
                isValid = false;
            }

            if (!isValid)
            {
                try
                {
                    Uri uri = new Uri($"http://{address}");

                    if (uri.HostNameType == UriHostNameType.IPv4 || uri.HostNameType == UriHostNameType.IPv6)
                    {
                        if (uri.ToString().ToLower() != address.ToLower())
                        {
                            isValid = false;
                        }
                    }
                    else
                    {
                        isValid = true;
                    }
                }
                catch (Exception)
                {
                    isValid = false;
                }
            }

            if (!isValid)
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
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                    }
                }
                catch (Exception)
                {
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}
