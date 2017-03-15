using System.Collections.Generic;
using PostcodeServices.Models;

namespace PostcodeServices.ValidationService
{
    public interface IPostcodeValidationService
    {
        bool ValidatePostcode(string postcode);

        IList<Postcode> ValidatePostcodeCollection(IList<Postcode> postcodeCollection);
    }
}
