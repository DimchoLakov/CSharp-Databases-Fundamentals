using BusTicket.Models;

namespace BusTicket.Services.Contracts
{
    public interface IBusStationService
    {
        void AddBusStation(string name, int townId);

        BusStation GetBusStationById(int busStationId);

        BusStation GetBusStationByName(string busStationName);

        bool Exists(string name);

        bool Exists(int busStationId);
    }
}
