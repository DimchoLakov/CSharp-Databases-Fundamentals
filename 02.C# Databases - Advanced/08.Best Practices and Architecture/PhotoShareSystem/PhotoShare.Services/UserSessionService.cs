using System;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class UserSessionService : IUserSessionService
    {
        private const string UserNotLoggedIn = "User {0} not logged in!";
        private const string UserAlreadyLoggedIn = "User {0} already logged in!";

        private readonly IUserService _userService;

        public UserSessionService(IUserService userService)
        {
            this._userService = userService;
        }

        public User User { get; private set; }
        
        public bool IsLoggedIn => this.User != null;

        public void Login(string username)
        {
            if (this.IsLoggedIn)
            {
                throw new InvalidOperationException(string.Format(UserAlreadyLoggedIn, this.User.Username));
            }

            this.User = this._userService.ByUsername<User>(username);
        }

        public void Logout()
        {
            if (!this.IsLoggedIn)
            {
                throw new InvalidOperationException(string.Format(UserNotLoggedIn, this.User.Username));
            }
            
            this.User = null;
        }
    }
}
