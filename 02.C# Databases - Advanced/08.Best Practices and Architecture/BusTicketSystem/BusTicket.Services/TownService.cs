using System;
using System.Linq;
using BusTicket.Data;
using BusTicket.Models;
using BusTicket.Services.Contracts;

namespace BusTicket.Services
{
    public class TownService : ITownService
    {
        private readonly BusTicketContext _dbContext;

        public TownService(BusTicketContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public bool Exists(int townId)
        {
            return this.GetTownById(townId) != null;
        }

        public bool Exists(string name)
        {
            return this.GetTownByName(name) != null;
        }

        public void AddTown(string name, string country)
        {
            var town = new Town()
            {
                Name = name,
                Country = country
            };

            this._dbContext
                .Towns
                .Add(town);

            this._dbContext
                .SaveChanges();
        }

        public Town RemoveTown(string name)
        {
            var town = this.GetTownByName(name);

            this._dbContext
                .Towns
                .Remove(town);

            return town;
        }

        public Town GetTownByName(string name)
        {
            var town = this._dbContext
                .Towns
                .FirstOrDefault(x => x.Name == name);

            return town;
        }

        public Town GetTownById(int townId)
        {
            var town = this._dbContext
                .Towns
                .FirstOrDefault(x => x.TownId == townId);

            return town;
        }
    }
}
