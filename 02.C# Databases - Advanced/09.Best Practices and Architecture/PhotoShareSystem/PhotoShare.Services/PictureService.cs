namespace PhotoShare.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Models;
    using Contracts;

    public class PictureService : IPictureService
    {
        private readonly PhotoShareContext _context;

        public PictureService(PhotoShareContext context)
        {
            this._context = context;
        }

        public TModel ById<TModel>(int id)
                => By<TModel>(a => a.Id == id).SingleOrDefault();

        public TModel ByTitle<TModel>(string name)
            => By<TModel>(a => a.Title == name).SingleOrDefault();

        public bool Exists(int id)
            => ById<Picture>(id) != null;

        public bool Exists(string name)
           => ByTitle<Picture>(name) != null;

        private IEnumerable<TModel> By<TModel>(Func<Picture, bool> predicate) =>
            this._context.Pictures
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();

        public Picture Create(int albumId, string pictureTitle, string pictureFilePath)
        {
            var picture = new Picture()
            {
                Title = pictureTitle,
                Path = pictureFilePath,
                AlbumId = albumId
            };

            this._context.Pictures.Add(picture);

            this._context.SaveChanges();

            return picture;
        }
    }
}
