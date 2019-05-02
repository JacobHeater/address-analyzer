using System.Net;
using System.Web.Http;
using AddressAnalyzer.Common.Validation;
using Microsoft.AspNetCore.Mvc;

namespace AddressAnalyzer.Common.Extensions
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// Uses the AddressValidator class to validate the
        /// address, and throws an HttpResponseException with
        /// a BadRequest code if the input is invalid.
        /// </summary>
        /// <param name="controller">Controller to extend.</param>
        /// <param name="address">Address to validate.</param>
        public static void ValidateAddressInput(this Controller controller, string address)
        {
            if (!AddressValidator.IsAddressValid(address))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}
