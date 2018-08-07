namespace PhotoShare.Services
{
    using System;

    using Models;
    using Models.Enums;
    using Data;
    using Contracts;

    public class AlbumRoleService : IAlbumRoleService
    {
        private readonly PhotoShareContext _dbContext;

        public AlbumRoleService(PhotoShareContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public AlbumRole PublishAlbumRole(int albumId, int userId, string role)
        {
            var roleAsEnum = Enum.Parse<Role>(role);

            var albumRole = new AlbumRole()
            {
                AlbumId = albumId,
                UserId = userId,
                Role = roleAsEnum
            };

            this._dbContext.AlbumRoles.Add(albumRole);

            this._dbContext.SaveChanges();

            return albumRole;
        }
    }
}
