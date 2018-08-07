using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class TagService : ITagService
    {
        private readonly PhotoShareContext _dbContext;

        public TagService(PhotoShareContext dbContext)
        {
            this._dbContext = dbContext;
        }

        private IEnumerable<TModel> By<TModel>(Func<Tag, bool> predicate)
        {
            return this._dbContext
                .Tags
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();
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
            return this.ById<Tag>(id) != null;
        }

        public bool Exists(string name)
        {
            return this.ByName<Tag>(name) != null;
        }

        public Tag AddTag(string name)
        {
            Tag tag = new Tag
            {
                Name = name
            };

            this._dbContext
                .Tags
                .Add(tag);

            this._dbContext.SaveChanges();

            return tag;
        }
    }
}
