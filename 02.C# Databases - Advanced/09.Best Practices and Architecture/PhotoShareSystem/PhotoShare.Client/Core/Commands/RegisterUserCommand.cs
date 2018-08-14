using PhotoShare.Client.Core.Dtos;
using PhotoShare.Data;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class RegisterUserCommand : ICommand
    {
        private const string InvalidData = "INVALID DATA ENTERED!";
        private const string UsernameAlreadyTaken = "Username {0} is already taken!";
        private const string PasswordsDoNotMatch = "Passwords do not match";
        private const string UserLoggedIn = "You are already logged in! In order to register please logout.";
        private const string SuccessfullyRegisteredUser = "User {0} was registered successfully!";

        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;

        public RegisterUserCommand(IUserService userService, IUserSessionService userSessionService)
        {
            this._userService = userService;
            this._userSessionService = userSessionService;
        }

        // RegisterUser <username> <password> <repeat-password> <email>
        public string Execute(string[] data)
        {
            string username = data[0];
            string password = data[1];
            string confirmPassword = data[2];
            string email = data[3];

            var isLoggedIn = this._userSessionService.IsLoggedIn;

            if (isLoggedIn)
            {
                throw new InvalidOperationException(UserLoggedIn);
            }

            RegisterUserDto regUserDto = new RegisterUserDto()
            {
                Username = username,
                Email = email,
                Password = password,
                RepeatPassword = confirmPassword
            };

            if (this._userService.Exists(username))
            {
                throw new InvalidOperationException(string.Format(UsernameAlreadyTaken, username));
            }

            if (!IsValid(regUserDto))
            {
                throw new ArgumentException(InvalidData);
            }

            if (password != confirmPassword)
            {
                throw new ArgumentException(PasswordsDoNotMatch);
            }

            this._userService.Register(username, password, email);

            return string.Format(SuccessfullyRegisteredUser, username);
        }

        private bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}
