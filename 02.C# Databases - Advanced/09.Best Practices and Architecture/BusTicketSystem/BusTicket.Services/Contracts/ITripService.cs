using System;
using BusTicket.Models;
using BusTicket.Models.Enums;

namespace BusTicket.Services.Contracts
{
    public interface ITripService
    {
        void AddTrip(TimeSpan departureTime, TimeSpan arrivalTime, TripStatus status, string busCompanyName, string originBusStation, string destinatonBusStation);

        Trip RemoveTrip(int tripId);

        Trip GetTripById(int tripId);

        bool Exists(int tripId);
    }
}
