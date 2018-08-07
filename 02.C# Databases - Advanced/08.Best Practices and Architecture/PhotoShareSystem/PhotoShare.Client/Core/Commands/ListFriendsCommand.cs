using System;
using System.Linq;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Data;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class ListFriendsCommand : ICommand
    {
        private const string UserDoesNotExist = "User {0} does not exist!";
        private const string NoFriendsForUser = "There is no friends for {0}. :(";
        private const string UserLoggedIn = "You are already logged in! In order to register please logout.";

        private readonly IUserService _userService;

        public ListFriendsCommand(IUserService userService)
        {
            this._userService = userService;
        }

        public string Execute(string[] args)
        {
            var username = args[0];

            var userExists = this._userService.Exists(username);

            if (!userExists)
            {
                throw new ArgumentException(string.Format(UserDoesNotExist, username));
            }
            
            var user = this._userService.ByUsername<UserFriendsDto>(username);

            if (user.Friends.Count == 0)
            {
                return string.Format(NoFriendsForUser, username);
            }

            var friendUsernames = user.Friends.Select(f => f.Username).ToArray();
            var result = string.Join(Environment.NewLine, friendUsernames);
            return result;
        }
    }
}
