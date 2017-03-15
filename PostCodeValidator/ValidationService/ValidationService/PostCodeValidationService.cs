using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using PostcodeServices.Models;

namespace PostcodeServices.ValidationService
{
    /// <summary>
    /// Postcode validation service that provides a range of 
    /// postcode validation helper functions.
    /// </summary>
    public class PostcodeValidationService : IPostcodeValidationService
    {
        #region Private Members
        private const string ValidationPattern = @"^(GIR\s0AA)|^((([A-PR-UWYZ][0-9][0-9]?)|(([A-PR-UWYZ][A-HK-Y][0-9](?<!(BR|FY|HA|HD|HG|HR|HS|HX|JE|LD|SM|SR|WC|WN|ZE)[0-9])[0-9])|([A-PR-UWYZ][A-HK-Y](?<!AB|LL|SO)[0-9])|(WC[0-9][A-Z])|(([A-PR-UWYZ][0-9][A-HJKPSTUW])|([A-PR-UWYZ][A-HK-Y][0-9][ABEHMNPRVWXY]))))\s[0-9][ABD-HJLNP-UW-Z]{2})";
        //private static readonly Regex RegularExpression = new Regex(ValidationPattern);

        // Task 3 optimistation
        private static readonly Regex RegularExpression = new Regex(ValidationPattern, RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Validates a string input that represents a postcode.
        /// </summary>
        /// <param name="postcode">Postcde to be validated.</param>
        /// <returns>Bool that represents the result.</returns>
        public bool ValidatePostcode(string postcode)   
        {
            if (string.IsNullOrEmpty(postcode))
            {
                throw new ArgumentNullException(postcode);
            }

            return RegularExpression.IsMatch(postcode.ToUpper());
        }

        /// <summary>
        /// Validates a postcode object.
        /// </summary>
        /// <param name="postcode">A valid postcode object.</param>
        /// <returns>Bool that indicates the result.</returns>
        public bool ValidatePostcode(Postcode postcode)
        {
            if (postcode == null)
            {
                throw new ArgumentNullException(nameof(postcode));
            }

            return ValidatePostcode(postcode.Value);
        }

        /// <summary>
        /// Validates a collection of postcode objects
        /// </summary>
        /// <param name="postcodeCollection">An IEnumerable list of postcode objects.</param>
        /// <returns>A updates IEnumerable list of postcode objects.</returns>
        public IList<Postcode> ValidatePostcodeCollection(IList<Postcode> postcodeCollection)
        {
            if (postcodeCollection == null)
            {
                throw new ArgumentNullException(nameof(postcodeCollection));
            }

            foreach (var postcode in postcodeCollection)
            {
                postcode.IsValid = ValidatePostcode(postcode);
            }

            return postcodeCollection;
        }
    }
}
