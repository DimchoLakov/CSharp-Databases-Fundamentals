using System;
using System.ComponentModel.DataAnnotations;

namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NonUnicodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMsg = "Property value cannot be null!";

            if (value == null)
            {
                return new ValidationResult(errorMsg);
            }

            string propertyValue = value.ToString();

            foreach (char c in propertyValue)
            {
                if (c > 255)
                {
                    string unicodeErrorMsg = "Property value cannot contain unicode symbols!";
                    return new ValidationResult(unicodeErrorMsg);
                }
            }

            return ValidationResult.Success;
        }
    }
}
