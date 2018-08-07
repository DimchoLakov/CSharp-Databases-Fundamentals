using System;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class LogoutCommand : ICommand
    {
        private const string SuccessfullyLoggedIn = "User {0} successfully logged out!";
        private const string NotLoggedIn = "You should log in first in order to logout.";

        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;

        public LogoutCommand(IUserSessionService userSessionService, IUserService userService)
        {
            this._userService = userService;
            this._userSessionService = userSessionService;
        }

        public string Execute(string[] args)
        {
            var isUserLoggedIn = this._userSessionService.IsLoggedIn;

            if (!isUserLoggedIn)
            {
                throw new InvalidOperationException(NotLoggedIn);
            }

            var username = this._userSessionService.User.Username;

            this._userSessionService.Logout();

            return string.Format(SuccessfullyLoggedIn, username);
        }
    }
}
