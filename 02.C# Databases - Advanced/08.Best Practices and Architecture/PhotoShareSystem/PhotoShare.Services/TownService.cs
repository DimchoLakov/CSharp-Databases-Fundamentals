using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class TownService : ITownService
    {
        private readonly PhotoShareContext _dbContext;

        public TownService(PhotoShareContext dbContext)
        {
            this._dbContext = dbContext;
        }

        private IEnumerable<TModel> By<TModel>(Func<Town, bool> predicate)
        {
            return this._dbContext
                .Towns
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
            return this.ById<Town>(id) != null;
        }

        public bool Exists(string name)
        {
            return this.ByName<Town>(name) != null;
        }

        public Town Add(string townName, string countryName)
        {
            Town town = new Town
            {
                Name = townName,
                Country = countryName
            };

            this._dbContext
                .Towns
                .Add(town);

            this._dbContext.SaveChanges();

            return town;
        }
    }
}
