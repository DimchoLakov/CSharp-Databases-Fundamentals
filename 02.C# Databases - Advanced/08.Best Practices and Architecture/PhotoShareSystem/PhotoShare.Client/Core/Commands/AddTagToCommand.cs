using PhotoShare.Client.Core.Dtos;
using PhotoShare.Client.Utilities;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;

    public class AddTagToCommand : ICommand
    {
        private const string TagOrAlbumDoNotExist = "Either tag or album do not exist!";
        private const string InvalidCredentials = "Invalid credentials!";
        private const string SuccessfullyAddedTagToAlbum = "Tag {0} added to {1}!";

        private readonly ITagService _tagService;
        private readonly IAlbumService _albumService;
        private readonly IAlbumTagService _albumTagService;
        private readonly IUserSessionService _userSessionService;

        public AddTagToCommand(ITagService tagService, IAlbumService albumService, IAlbumTagService albumTagService, IUserSessionService userSessionService)
        {
            this._tagService = tagService;
            this._albumService = albumService;
            this._albumTagService = albumTagService;
            this._userSessionService = userSessionService;
        }

        // AddTagTo <albumName> <tag>
        public string Execute(string[] args)
        {
            var isLoggedIn = this._userSessionService.IsLoggedIn;

            if (!isLoggedIn)
            {
                throw new InvalidOperationException(InvalidCredentials);
            }

            string albumName = args[0];
            string tagName = args[1].ValidateOrTransform();

            bool albumExists = this._albumService.Exists(albumName);
            bool tagExists = this._tagService.Exists(tagName);

            if (!albumExists || !tagExists)
            {
                throw new ArgumentException(TagOrAlbumDoNotExist);
            }

            int albumId = this._albumService.ByName<AlbumDto>(albumName).Id;
            int tagId = this._tagService.ByName<TagDto>(tagName).Id;
            
            this._albumTagService.AddTagTo(albumId, tagId);

            return string.Format(SuccessfullyAddedTagToAlbum, tagName, albumName);
        }
    }
}
