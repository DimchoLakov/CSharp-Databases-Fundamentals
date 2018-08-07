namespace PhotoShare.Client.Core.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class PasswordAttribute : ValidationAttribute
    {
        private const string SpecialSymbols = "!@#$%^&*()_+<>,.?";
        private readonly int _minLength;
        private readonly int _maxLength;

        public PasswordAttribute(int minLength, int maxLength)
        {
            this._minLength = minLength;
            this._maxLength = maxLength;
        }

        public override bool IsValid(object value)
        {
            string password = value.ToString();

            if (password.Length < this._minLength || password.Length > this._maxLength)
            {
                return false;
            }

            if (!password.Any(char.IsLower))
            {
                return false;
            }

            if (!password.Any(char.IsUpper))
            {
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                return false;
            }

            if (!password.Any(c => SpecialSymbols.Contains(c)))
            {
                return false;
            }

            return true;
        }
    }
}
