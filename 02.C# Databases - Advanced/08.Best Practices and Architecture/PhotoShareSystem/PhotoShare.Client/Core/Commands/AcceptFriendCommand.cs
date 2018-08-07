using System.Linq;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;

    public class AcceptFriendCommand : ICommand
    {
        private const string UserNotFound = "{0} not found!";
        private const string FriendRequestNotSent = "Friend request has not been sent! {1} has not added {0} as a friend.";
        private const string AlreadyFriends = "Users {0} and {1} are already friends!";
        private const string RequestAlreadySent = "A request has already been sent from {0} to {1}.";
        private const string CannotAcceptRequestFromYourself = "Cannot accept request from yourself!";
        private const string InvalidCredentials = "Invalid credentials!";
        private const string SuccessfullyAcceptedFriendRequest = "{0} accepted {1} as a friend.";

        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;

        public AcceptFriendCommand(IUserService userService, IUserSessionService userSessionService)
        {
            this._userService = userService;
            this._userSessionService = userSessionService;
        }

        // AcceptFriend <username1> <username2>
        public string Execute(string[] data)
        {
            string userName = data[0];
            string friendName = data[1];

            var isLoggedIn = this._userSessionService.IsLoggedIn;
            var isSamePerson = this._userSessionService.User.Username == userName;

            if (!isLoggedIn || !isSamePerson)
            {
                throw new InvalidOperationException(InvalidCredentials);
            }

            bool userЕxist = this._userService.Exists(userName);
            bool friendЕxist = this._userService.Exists(friendName);

            if (!userЕxist)
            {
                throw new ArgumentException(string.Format(UserNotFound, userName));
            }

            if (!friendЕxist)
            {
                throw new ArgumentException(string.Format(UserNotFound, userName));
            }

            int userId = this._userService.ByUsername<UserDto>(userName).Id;
            int friendId = this._userService.ByUsername<UserDto>(friendName).Id;

            var user = this._userService.ById<UserFriendsDto>(userId);
            var friend = this._userService.ById<UserFriendsDto>(friendId);

            bool isRequestSent = friend.Friends.Any(u => u.Username == userName);
            bool isRequestAccepted = user.Friends.Any(u => u.Username == friendName);

            if (!isRequestSent)
            {
                throw new InvalidOperationException(string.Format(FriendRequestNotSent, friendName, userName));
            }

            if (isRequestSent && isRequestAccepted)
            {
                throw new InvalidOperationException(string.Format(AlreadyFriends, userName, friendName));
            }

            if (!isRequestSent && isRequestAccepted)
            {
                throw new InvalidOperationException(string.Format(RequestAlreadySent, userName, friendName));
            }

            if (userId == friendId)
            {
                throw new InvalidOperationException(CannotAcceptRequestFromYourself);
            }

            this._userService.AcceptFriend(userId, friendId);

            return string.Format(SuccessfullyAcceptedFriendRequest, userName, friendName);
        }
    }
}
