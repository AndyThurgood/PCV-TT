using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using PostcodeServices.Models;
using PostcodeServices.ValidationService;

namespace ValidationTests
{
    /// <summary>
    /// Unit tests that focus on testing the regular expression used by
    /// the PostcodeValidationService.
    /// </summary>
    [TestClass]
    public class PostcodeValidationServiceTests
    {
        private static IPostcodeValidationService _validationService;

        [TestInitialize]
        public void Initialize()
        {
            _validationService = new PostcodeValidationService();
        }

        [TestMethod]
        public void ValidationService_ShouldReject_JunkPostcodes()
        {
            bool result = _validationService.ValidatePostcode("$%± ()()");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationService_ShouldReject_InvalidPostcodes()
        {
            bool result = _validationService.ValidatePostcode("XX XXX");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationService_ShouldReject_ShortPostcodes()
        {
            bool result = _validationService.ValidatePostcode("A1 9A");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationService_ShouldReject_NonWhitespacePostcodes()
        {
            bool result = _validationService.ValidatePostcode("LS44PL");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationService_ShouldReject_Q_PrefixedPostcodes()
        {
            bool result = _validationService.ValidatePostcode("Q1A 9AA");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationService_ShouldReject_V_PrefixedPostcodes()
        {
            bool result = _validationService.ValidatePostcode("V1A 9AA");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationService_ShouldReject_X_PrefixedPostcodes()
        {
            bool result = _validationService.ValidatePostcode("X1A 9BB");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationService_ShouldReject_I_PrefixedPostcodes()
        {
            bool result = _validationService.ValidatePostcode("LI10 3QP");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationService_ShouldReject_J_PrefixedPostcodes()
        {
            bool result = _validationService.ValidatePostcode("LJ10 3QP");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationService_ShouldReject_Z_PrefixedPostcodes()
        {
            bool result = _validationService.ValidatePostcode("LZ10 3QP");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationService_ShouldReject_Q_AsThirdCharecterPostcodes()
        {
            bool result = _validationService.ValidatePostcode("A9Q 9AA");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationService_ShouldReject_C_AsFourthCharecterPostcodes()
        {
            bool result = _validationService.ValidatePostcode("AA9C 9AA");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationService_ShouldReject_SingleDigitOnlyPostcodes()
        {
            bool result = _validationService.ValidatePostcode("FY10 4PL");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationService_ShouldReject_DoubleDigitOnlyPostcodes()
        {
            bool result = _validationService.ValidatePostcode("SO1 4QQ");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationService_ShouldAccept_ValidPostcodes()
        {
            IList<Postcode> validPostCodes = new List<Postcode>
            {
                new Postcode(1,"EC1A 1BB"),
                new Postcode(2,"W1A 0AX"),
                new Postcode(3,"M1 1AE"),
                new Postcode(4,"B33 8TH"),
                new Postcode(5,"CR2 6XH"),
                new Postcode(6,"DN55 1PT"),
                new Postcode(7,"GIR 0AA"),
                new Postcode(8,"SO10 9AA"),
                new Postcode(9, "FY9 9AA"),
                new Postcode(10,"WC1A 9AA")
            };

            validPostCodes = _validationService.ValidatePostcodeCollection(validPostCodes);

            bool validationResult =  validPostCodes.All(x => x.IsValid);
            Assert.IsTrue(validationResult);
        }
    }
}
