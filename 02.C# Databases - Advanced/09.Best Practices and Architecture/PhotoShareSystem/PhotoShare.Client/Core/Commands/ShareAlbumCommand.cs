using PhotoShare.Client.Core.Dtos;
using PhotoShare.Models.Enums;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;

    public class ShareAlbumCommand : ICommand
    {
        private const string AlbumDoesNotExist = "Album with id {0} not found!";
        private const string UserDoesNotExist = "User {0} not found!";
        private const string PermissionNotValid = "Permission must be either \"Owner\" or \"Viewer\"";
        private const string UserAlreadyConnectedToAlbum = "User {!} already linked to album {0}.";
        private const string InvalidCredentials = "Invalid credentials!";
        private const string SuccessfullyPublishedAlbum = "Username {1} added to album {0} ({2})";

        private readonly IAlbumRoleService _albumRoleService;
        private readonly IAlbumService _albumService;
        private readonly IUserSessionService _userSessionService;
        private readonly IUserService _userService;

        public ShareAlbumCommand(IAlbumRoleService albumRoleService, IAlbumService albumService, IUserService userService, IUserSessionService userSessionService)
        {
            this._albumRoleService = albumRoleService;
            this._albumService = albumService;
            this._userService = userService;
            this._userSessionService = userSessionService;
        }
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(string[] data)
        {
            var albumId = int.Parse(data[0]);
            var username = data[1];
            var permission = data[2];

            var isLoggedIn = this._userSessionService.IsLoggedIn;
            var isSamePerson = this._userSessionService.User.Username == username;

            if (!isLoggedIn || !isSamePerson)
            {
                throw new InvalidOperationException(InvalidCredentials);
            }

            var albumExists = this._albumService.Exists(albumId);

            if (!albumExists)
            {
                throw new ArgumentException(string.Format(AlbumDoesNotExist, albumId));
            }

            var userExists = this._userService.Exists(username);

            if (!userExists)
            {
                throw new ArgumentException(string.Format(UserDoesNotExist, username));
            }

            var isPermissionValid = Enum.TryParse(permission, true, out Role role);

            if (!isPermissionValid)
            {
                throw new ArgumentException(PermissionNotValid);
            }
            
            var albumName = this._albumService.ById<AlbumDto>(albumId).Name;

            var userId = this._userService.ByUsername<UserDto>(username).Id;

            this._albumRoleService.PublishAlbumRole(albumId, userId, permission);

            return string.Format(SuccessfullyPublishedAlbum, albumName, username, permission);
        }
    }
}
