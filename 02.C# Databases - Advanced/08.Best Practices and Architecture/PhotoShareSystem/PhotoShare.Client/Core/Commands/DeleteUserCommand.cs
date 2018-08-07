namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Dtos;
    using Contracts;
    using Services.Contracts;

    public class DeleteUserCommand : ICommand
    {
        private const string UsernameNotFound = "User {0} not found!";
        private const string UserAlreadyDeleted = "User {0} is already deleted!";
        private const string InvalidCredentials = "Invalid credentials!";
        private const string SuccessfullyDeletedUser = "User {0} was deleted successfully!";

        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;

        public DeleteUserCommand(IUserService userService, IUserSessionService userSessionService)
        {
            this._userService = userService;
            this._userSessionService = userSessionService;
        }

        // DeleteUser <username>
        public string Execute(string[] data)
        {
            string username = data[0];

            var isLoggedIn = this._userSessionService.IsLoggedIn;
            var isSamePerson = this._userSessionService.User.Username == username;

            if (!isLoggedIn || !isSamePerson)
            {
                throw new InvalidOperationException(InvalidCredentials);
            }

            var userExists = this._userService.Exists(username);

            if (!userExists)
            {
                throw new ArgumentException(string.Format(UsernameNotFound, username));
            }

            var user = this._userService.ByUsername<UserDto>(username);

            if (user.IsDeleted == true)
            {
                throw new InvalidOperationException(string.Format(UserAlreadyDeleted, username));
            }
            
            this._userService.Delete(username);

            return string.Format(SuccessfullyDeletedUser, username);
        }
    }
}
