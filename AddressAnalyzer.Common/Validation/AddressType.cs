namespace AddressAnalyzer.Common.Validation
{
    /// <summary>
    /// Defines the different address types that
    /// Address Analyzer can work with, and parse.
    /// </summary>
    public enum AddressType
    {
        /// <summary>
        /// IP Address.
        /// </summary>
        IPAddress,
        /// <summary>
        /// Domain.
        /// </summary>
        Domain,
        /// <summary>
        /// Unknown address type.
        /// </summary>
        Unknown
    }
}
