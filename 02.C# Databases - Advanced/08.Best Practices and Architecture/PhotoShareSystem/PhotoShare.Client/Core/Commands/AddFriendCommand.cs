using System.Linq;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Data;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;

    public class AddFriendCommand : ICommand
    {
        private const string UserNotFound = "{0} not found!";
        private const string FriendRequestSent = "Friend request from {0} to {1} already sent!";
        private const string AlreadyFriends = "Users {0} and {1} are already friends";
        private const string FriendRequestReceived = "User {0} has already received a friend request from {1}";
        private const string CannotSendFriendRequestToYourself = "Cannot send friend request to yourself!";
        private const string InvalidCredentials = "Invalid credentials!";
        private const string SuccessfullyAddedFriendship = "Friend {0} added to {1}.";

        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;

        public AddFriendCommand(IUserService userService, IUserSessionService userSessionService)
        {
            this._userService = userService;
            this._userSessionService = userSessionService;
        }

        // AddFriend <username1> <username2>
        public string Execute(string[] data)
        {
            string firstUsername = data[0];
            string secondUsername = data[1];

            bool firstUserExists = this._userService.Exists(firstUsername);
            bool secondUserExists = this._userService.Exists(secondUsername);

            if (!firstUserExists)
            {
                throw new ArgumentException(string.Format(UserNotFound, firstUsername));
            }

            if (!secondUserExists)
            {
                throw new ArgumentException(string.Format(UserNotFound, secondUsername));
            }

            var isLoggedIn = this._userSessionService.IsLoggedIn;

            if (!isLoggedIn)
            {
                throw new InvalidOperationException(InvalidCredentials);
            }

            var isSamePerson = this._userSessionService.User.Username == firstUsername;

            if (!isSamePerson)
            {
                throw new InvalidOperationException(InvalidCredentials);
            }

            int firstUserId = this._userService.ByUsername<UserDto>(firstUsername).Id;
            int secondUserId = this._userService.ByUsername<UserDto>(secondUsername).Id;
            
            var firstUser = this._userService.ById<UserFriendsDto>(firstUserId);
            var secondUser = this._userService.ById<UserFriendsDto>(secondUserId);

            bool alreadyAdded = firstUser.Friends.Any(u => u.Username == secondUser.Username);
            bool accepted = secondUser.Friends.Any(u => u.Username == firstUser.Username);

            if (alreadyAdded && !accepted)
            {
                throw new InvalidOperationException(string.Format(FriendRequestSent, firstUsername, secondUsername));
            }

            if (alreadyAdded && accepted)
            {
                throw new InvalidOperationException(string.Format(AlreadyFriends, firstUsername, secondUsername));
            }

            if (!alreadyAdded && accepted)
            {
                throw new InvalidOperationException(string.Format(FriendRequestReceived, firstUsername, secondUsername));
            }

            if (firstUserId == secondUserId)
            {
                throw new InvalidOperationException(CannotSendFriendRequestToYourself);
            }

            this._userService.AddFriend(firstUserId, secondUserId);

            return string.Format(SuccessfullyAddedFriendship, secondUsername, firstUsername);
        }
    }
}
