using System;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private const string UsernameOrPasswordDoNotMatch = "Invalid username or password!";
        private const string AlreadyLoggedIn = "You should logout first.";
        private const string SuccessfullyLoggedIn = "User {0} successfully logged in.";

        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;

        public LoginCommand(IUserSessionService userSessionService, IUserService userService)
        {
            this._userService = userService;
            this._userSessionService = userSessionService;
        }

        // Login <username> <password>
        public string Execute(string[] args)
        {
            var username = args[0];
            var password = args[1];

            var isLoggedIn = this._userSessionService.IsLoggedIn;

            if (isLoggedIn)
            {
                throw new ArgumentException(AlreadyLoggedIn);
            }

            var userExists = this._userService.Exists(username);
            if (!userExists)
            {
                throw new ArgumentException(UsernameOrPasswordDoNotMatch);
            }

            var userDto = this._userService.ByUsername<UserDto>(username);
            var passwordMatches = userDto.Password.Equals(password);

            if (!passwordMatches)
            {
                throw new ArgumentException(UsernameOrPasswordDoNotMatch); 
            }

            this._userSessionService.Login(username);

            return string.Format(SuccessfullyLoggedIn, username);
        }
    }
}
