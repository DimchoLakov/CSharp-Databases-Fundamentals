using BusTicket.Models;

namespace BusTicket.Services.Contracts
{
    public interface IBusCompanyService
    {
        void CreateBusCompany(string name, string nationality);

        BusCompany GetBusCompanyById(int busCompanyId);

        BusCompany GetBusCompanyByName(string name);

        BusCompany RemoveBusCompany(int busCompanyId);

        void AddReview(Review review, int busCompanyId);

        void AddTrip(Trip trip, int busCompanyId);

        string PrintReviews(int busCompanyId);

        bool Exists(string name);
    }
}
