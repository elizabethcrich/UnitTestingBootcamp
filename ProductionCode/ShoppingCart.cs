using System;

namespace ProductionCode
{
    public class ShoppingCart
    {
        private readonly IZipCodeValidator _zipCodeValidator;

        public ShoppingCart(IZipCodeValidator zipCodeValidator)
        {
            _zipCodeValidator = zipCodeValidator;
        }

        public double GetSalesTaxAmount(string zipcode)
        {
            if (!_zipCodeValidator.Validate(zipcode))
            {
                throw new ArgumentException("Invalid ZipCode", nameof(zipcode));
            }

            return 0.0;
        }
    }
}
