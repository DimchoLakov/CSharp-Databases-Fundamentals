using System.Linq;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;

    public class ModifyUserCommand : ICommand
    {
        private const string UserDoesNotExist = "User {0} not found!";
        private const string PropertyNotFound = "Property {0} not supported!";
        private const string InvalidPassword = "Invalid Password!";
        private const string TownNotFound = "Town {0} not found!";
        private const string InvalidCredentials = "Invalid credentials!";
        private const string SuccessfullySetNewValue = "User {0} {1} is {2}.";

        private readonly IUserService _userService;
        private readonly ITownService _townService;
        private readonly IUserSessionService _userSessionService;

        public ModifyUserCommand(IUserService userService, ITownService townService, IUserSessionService userSessionService)
        {
            this._userService = userService;
            this._townService = townService;
            this._userSessionService = userSessionService;
        }

        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            string username = data[0];
            string propertyName = data[1];
            string newPropertyValue = data[2];

            var isLoggedIn = this._userSessionService.IsLoggedIn;
            var isSamePerson = this._userSessionService.User.Username == username;

            if (!isLoggedIn || !isSamePerson)
            {
                throw new InvalidOperationException(InvalidCredentials);
            }

            string propertyNameToLower = propertyName.ToLower();

            bool userExists = this._userService.Exists(username);

            if (!userExists)
            {
                throw new ArgumentException(string.Format(UserDoesNotExist, username));
            }

            int userId = this._userService.ByUsername<UserDto>(username).Id;

            string[] allowedProperties = new string[] { "password", "borntown", "currenttown" };

            if (!allowedProperties.Contains(propertyNameToLower))
            {
                throw new ArgumentException(string.Format(PropertyNotFound, propertyName));
            }

            if (propertyNameToLower == "password")
            {
                if (!IsPasswordValid(newPropertyValue))
                {
                    throw new ArgumentException(InvalidPassword);
                }

                this._userService.ChangePassword(userId, newPropertyValue);

                return string.Format(SuccessfullySetNewValue, username, propertyName, newPropertyValue);
            }

            bool townExists = this._townService.Exists(newPropertyValue);

            if (!townExists)
            {
                throw new ArgumentException(string.Format(TownNotFound, newPropertyValue));
            }

            int townId = this._townService.ByName<TownDto>(newPropertyValue).Id;

            if (propertyNameToLower == "borntown")
            {
                this._userService.SetBornTown(userId, townId);
                return string.Format(SuccessfullySetNewValue, username, propertyName, newPropertyValue);
            }

            if (propertyNameToLower == "currenttown")
            {
                this._userService.SetCurrentTown(userId, townId);
            }
            return string.Format(SuccessfullySetNewValue, username, propertyName, newPropertyValue);
        }

        private bool IsPasswordValid(string newPropertyValue)
        {
            return newPropertyValue.Any(char.IsLower) && newPropertyValue.Any(char.IsDigit);
        }
    }
}
