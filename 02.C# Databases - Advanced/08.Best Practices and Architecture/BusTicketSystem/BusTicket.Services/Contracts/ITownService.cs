using BusTicket.Models;

namespace BusTicket.Services.Contracts
{
    public interface ITownService
    {
        void AddTown(string name, string country);

        Town RemoveTown(string name);

        Town GetTownById(int townId);

        Town GetTownByName(string name);

        bool Exists(int townId);

        bool Exists(string townName);
    }
}
