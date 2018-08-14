using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class AlbumTagService : IAlbumTagService
    {
        private readonly PhotoShareContext _dbContext;

        public AlbumTagService(PhotoShareContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public AlbumTag AddTagTo(int albumId, int tagId)
        {
            AlbumTag albumTag = new AlbumTag()
            {
                AlbumId = albumId,
                TagId = tagId
            };

            this._dbContext
                .AlbumTags
                .Add(albumTag);

            this._dbContext.SaveChanges();

            return albumTag;
        }
    }
}
