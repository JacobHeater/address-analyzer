using System;
namespace AddressAnalyzer.Common.DataProviders
{
    public class EmptyProviderUrlException : Exception
    {
        public EmptyProviderUrlException() : base("Provider URL was empty, and is required to retrieve data.")
        {
        }
    }
}
