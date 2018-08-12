using System;
using BusTicket.Client.Core.Contracts;
using BusTicket.Models.Enums;
using BusTicket.Services.Contracts;

namespace BusTicket.Client.Core.Commands
{
    public class AddTripCommand : IExecutable
    {
        private const string InvalidTime = "Invalid time format entered!";
        private const string InvalidStatus = "Invalid status!";
        private const string SuccessfullyAddedTrip = "Trip added successfully!";

        private readonly ITripService _tripService;

        public AddTripCommand(ITripService tripService)
        {
            this._tripService = tripService;
        }

        public string Execute(string[] args)
        {
            string departureTime = args[0];
            string arrivalTime = args[1];
            string status = args[2];
            string busCompanyName = args[3];
            string originBusStationName = args[4];
            string destinationBusStationName = args[5];

            if (!TimeSpan.TryParseExact(departureTime, @"hh\:mm", null, out TimeSpan depTime))
            {
                throw new ArgumentException(InvalidTime);
            }

            if (!TimeSpan.TryParseExact(arrivalTime, @"hh\:mm", null, out TimeSpan arrTime))
            {
                throw new ArgumentException(InvalidTime);
            }

            if (!Enum.TryParse(status, true, out TripStatus tripStatus))
            {
                throw new ArgumentException(InvalidStatus);
            }

            this._tripService.AddTrip(depTime, arrTime, tripStatus, busCompanyName, originBusStationName, destinationBusStationName);

            return SuccessfullyAddedTrip;
        }
    }
}
