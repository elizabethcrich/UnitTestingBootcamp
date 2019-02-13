namespace ProductionCode
{
    public class SimpleZipCodeValidator : IZipCodeValidator
    {
        public bool Validate(string zipcode)
        {
            return int.TryParse(zipcode, out int zip) 
                   && zip <= 99999 
                   && zip > 0;
        }
    }
}