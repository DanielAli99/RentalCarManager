using System.ComponentModel.DataAnnotations;

namespace RentalCarManager.CustomDataAnnotations
{
    internal class DynamicDateRange : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int yearValue)
            {
                int maximumRegistrationYear = DateTime.Now.Year + 1;
                if (yearValue < 1884 || yearValue > maximumRegistrationYear)
                {
                    return new ValidationResult($"Year of registration must be between 1884 and {maximumRegistrationYear}.");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid or missing year format.");
        }
    }
}
