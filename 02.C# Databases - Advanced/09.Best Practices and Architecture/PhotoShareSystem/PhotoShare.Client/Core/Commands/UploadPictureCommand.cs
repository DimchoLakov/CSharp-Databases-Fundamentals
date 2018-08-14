namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Dtos;
    using Contracts;
    using Services.Contracts;

    public class UploadPictureCommand : ICommand
    {
        private const string AlbumDoesNotExist = "Album {0} not found!";
        private const string InvalidCredentials = "Invalid credentials!";
        private const string SuccessfullyUploadedPicture = "Picture {0} added to {1}!";

        private readonly IPictureService _pictureService;
        private readonly IUserSessionService _userSessionService;
        private readonly IAlbumService _albumService;

        public UploadPictureCommand(IPictureService pictureService, IAlbumService albumService, IUserSessionService userSessionService)
        {
            this._pictureService = pictureService;
            this._albumService = albumService;
            this._userSessionService = userSessionService;
        }

        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(string[] data)
        {
            string albumName = data[0];
            string pictureTitle = data[1];
            string path = data[2];

            var isLoggedIn = this._userSessionService.IsLoggedIn;

            if (!isLoggedIn)
            {
                throw new InvalidOperationException(InvalidCredentials);
            }

            var albumExists = this._albumService.Exists(albumName);

            if (!albumExists)
            {
                throw new ArgumentException(string.Format(AlbumDoesNotExist, albumName));
            }

            var albumId = this._albumService.ByName<AlbumDto>(albumName).Id;

            this._pictureService.Create(albumId, pictureTitle, path);

            return string.Format(SuccessfullyUploadedPicture, pictureTitle, albumName);
        }
    }
}
