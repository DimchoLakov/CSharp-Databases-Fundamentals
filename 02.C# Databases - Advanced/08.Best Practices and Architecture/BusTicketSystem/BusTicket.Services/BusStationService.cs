using System.Linq;
using System.Text;
using BusTicket.Data;
using BusTicket.Models;
using BusTicket.Services.Contracts;

namespace BusTicket.Services
{
    public class BusStationService : IBusStationService
    {
        private readonly BusTicketContext _dbContext;
        private readonly ITownService _townService;

        public BusStationService(BusTicketContext dbContext, ITownService townService)
        {
            this._dbContext = dbContext;
            this._townService = townService;
        }

        public void AddBusStation(string name, int townId)
        {
            var town = this._townService.GetTownById(townId);

            var busStation = new BusStation()
            {
                Name = name,
                TownId = townId,
                Town = town
            };

            this._dbContext.BusStations.Add(busStation);
            this._dbContext.SaveChanges();
        }

        public BusStation GetBusStationById(int busStationId)
        {
            return this._dbContext
                .BusStations
                .FirstOrDefault(x => x.BusStationId == busStationId);
        }

        public BusStation GetBusStationByName(string busStationName)
        {
            return this._dbContext
                .BusStations
                .FirstOrDefault(x => x.Name == busStationName);
        }

        public bool Exists(string name)
        {
            return this.GetBusStationByName(name) != null;
        }

        public bool Exists(int busStationId)
        {
            return this.GetBusStationById(busStationId) != null;
        }
    }
}
