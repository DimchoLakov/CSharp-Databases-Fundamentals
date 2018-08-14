using System.Linq;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Models.Enums;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;
    using Services.Contracts;


    public class CreateAlbumCommand : ICommand
    {
        private const string UserNotFound = "User {0} not found!";
        private const string AlbumAlreadyExists = "Album {0} exists!";
        private const string InvalidTags = "Invalid tags!";
        private const string ColorDoesNotExist = "Color {0} not found!";
        private const string InvalidCredentials = "Invalid credentials!";
        private const string SuccessfullyCreatedAlbum = "Album {0} successfully created!";

        private readonly IAlbumService _albumService;
        private readonly IUserService _userService;
        private readonly ITagService _tagService;
        private readonly IUserSessionService _userSessionService;

        public CreateAlbumCommand(IAlbumService albumService, IUserService userService, ITagService tagService, IUserSessionService userSessionService)
        {
            this._albumService = albumService;
            this._userService = userService;
            this._tagService = tagService;
            this._userSessionService = userSessionService;
        }

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            string username = data[0];
            string albumTitle = data[1];
            var backgroundColor = data[2];
            string[] tags = data.Skip(3).ToArray();

            var isLoggedIn = this._userSessionService.IsLoggedIn;
            var isSamePerson = this._userSessionService.User.Username == username;

            if (!isLoggedIn || !isSamePerson)
            {
                throw new InvalidOperationException(InvalidCredentials);
            }
            
            bool userExists = this._userService.Exists(username);

            if (!userExists)
            {
                throw new ArgumentException(string.Format(UserNotFound, username));
            }

            bool albumExists = this._albumService.Exists(albumTitle);

            if (albumExists)
            {
                throw new ArgumentException(string.Format(AlbumAlreadyExists, albumTitle));
            }

            bool isColorValid = Enum.TryParse(backgroundColor, true, out Color bgColor);

            if (!isColorValid)
            {
                throw new ArgumentException(string.Format(ColorDoesNotExist, backgroundColor));
            }

            var existingTags = tags
                .Select(x => "#" + x)
                .Where(x => this._tagService.Exists(x))
                .ToArray();

            if (!existingTags.Any())
            {
                throw new ArgumentException(InvalidTags);
            }

            int userId = this._userService.ByUsername<UserDto>(username).Id;

            this._albumService.Create(userId, albumTitle, backgroundColor, existingTags);

            return string.Format(SuccessfullyCreatedAlbum, albumTitle);
        }
    }
}
