using System;
using System.ComponentModel.DataAnnotations;

namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class OneValidOneNotAttribute : ValidationAttribute
    {
        private readonly string _targetAttributeName;

        public OneValidOneNotAttribute(string targetAttributeName)
        {
            this._targetAttributeName = targetAttributeName;
        }

        protected override ValidationResult IsValid(object firstValue, ValidationContext validationContext)
        {
            var secondValue = validationContext
                                    .ObjectType
                                    .GetProperty(this._targetAttributeName)
                                    .GetValue(validationContext.ObjectInstance);

            if ((firstValue == null && secondValue != null) ||
                firstValue != null && secondValue == null)
            {
                return ValidationResult.Success;
            }

            string errorMsg = "One of the properties must be null and the other one - not null.";

            return new ValidationResult(errorMsg);
        }
    }
}
