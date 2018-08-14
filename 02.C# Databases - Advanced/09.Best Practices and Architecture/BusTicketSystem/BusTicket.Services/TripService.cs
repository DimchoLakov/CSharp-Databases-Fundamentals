using System;
using System.Linq;
using BusTicket.Data;
using BusTicket.Models;
using BusTicket.Models.Enums;
using BusTicket.Services.Contracts;

namespace BusTicket.Services
{
    public class TripService : ITripService
    {
        private readonly BusTicketContext _dbContext;
        private readonly IBusCompanyService _busCompanyService;
        private readonly IBusStationService _busStationService;

        public TripService(BusTicketContext dbContext, IBusCompanyService busCompany, IBusStationService busStationService)
        {
            this._dbContext = dbContext;
            this._busCompanyService = busCompany;
            this._busStationService = busStationService;
        }

        public bool Exists(int tripId)
        {
            return this.GetTripById(tripId) != null;
        }

        public void AddTrip(TimeSpan departureTime, TimeSpan arrivalTime, TripStatus status, string busCompanyName,
            string originBusStationName, string destinatonBusStationName)
        {
            var busCompany = this._busCompanyService
                .GetBusCompanyByName(busCompanyName);

            var originBusStation = this._busStationService
                .GetBusStationByName(originBusStationName);

            var destinationBusStation = this._busStationService
                .GetBusStationByName(destinatonBusStationName);

            var trip = new Trip()
            {
                DepartureTime = departureTime,
                ArrivalTime = arrivalTime,
                Status = status,
                BusCompany = busCompany,
                OriginBusStation = originBusStation,
                DestinationBusStation = destinationBusStation
            };

            this._dbContext
                .Trips.Add(trip);

            this._dbContext
                .SaveChanges();
        }

        public Trip RemoveTrip(int tripId)
        {
            var trip = this.GetTripById(tripId);

            this._dbContext
                .Trips
                .Remove(trip);

            this._dbContext
                .SaveChanges();

            return trip;
        }

        public Trip GetTripById(int tripId)
        {
            var trip = this._dbContext
                .Trips
                .FirstOrDefault(x => x.TripId == tripId);

            return trip;
        }
    }
}
