using System;
namespace AddressAnalyzer.Common.DataProviders
{
    /// <summary>
    /// This exception is encountered when the base provider
    /// was inherited, but the inheriting class did not
    /// return a value for the ProviderUrl property.
    /// </summary>
    public class EmptyProviderUrlException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:AddressAnalyzer.Common.DataProviders.EmptyProviderUrlException"/> class.
        /// </summary>
        public EmptyProviderUrlException() : base("Provider URL was empty, and is required to retrieve data.")
        {
        }
    }
}
