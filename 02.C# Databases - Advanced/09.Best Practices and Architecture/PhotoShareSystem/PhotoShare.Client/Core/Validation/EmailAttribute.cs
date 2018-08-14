namespace PhotoShare.Client.Core.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class EmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string email = value.ToString();

            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return email.Contains("@");
        }
    }
}
