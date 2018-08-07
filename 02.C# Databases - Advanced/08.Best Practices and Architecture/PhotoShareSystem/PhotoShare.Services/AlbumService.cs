using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Models.Enums;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
	public class AlbumService : IAlbumService
	{
	    private readonly PhotoShareContext _dbContext;

	    public AlbumService(PhotoShareContext dbContext)
	    {
	        this._dbContext = dbContext;
	    }

	    private IEnumerable<TModel> By<TModel>(Func<Album, bool> predicate)
	    {
	        var tModelCollection = this._dbContext
	            .Albums
	            .Where(predicate)
	            .AsQueryable()
	            .ProjectTo<TModel>();

	        return tModelCollection;
	    }

        public TModel ById<TModel>(int id)
        {
            return this.By<TModel>(x => x.Id == id)
                .SingleOrDefault();
        }

        public TModel ByName<TModel>(string name)
        {
            return this.By<TModel>(x => x.Name == name)
                .SingleOrDefault();
        }

        public bool Exists(int id)
        {
            return this.ById<Album>(id) != null;
        }

        public bool Exists(string name)
        {
            return this.ByName<Album>(name) != null;
        }

        public Album Create(int userId, string albumTitle, string backgroundColor, string[] tags)
        {
            Enum.TryParse<Color>(backgroundColor, true, out Color bgColor);
            
            Album album = new Album()
            {
                Name = albumTitle,
                BackgroundColor = bgColor
            };

            this._dbContext
                .Albums
                .Add(album);

            this._dbContext.SaveChanges();

            var albumTagCollection = new List<AlbumTag>();

            foreach (string tagName in tags)
            {
                var currentAlbumTag = this._dbContext
                    .Tags
                    .FirstOrDefault(x => x.Name == tagName);

                int currentAlbumTagId = currentAlbumTag.Id;

                AlbumTag albumTag = new AlbumTag()
                {
                    Album = album,
                    TagId = currentAlbumTagId
                };

                albumTagCollection.Add(albumTag);
            }

            this._dbContext
                .AlbumTags
                .AddRange(albumTagCollection);

            this._dbContext.SaveChanges();

            return album;
        }
    }
}
