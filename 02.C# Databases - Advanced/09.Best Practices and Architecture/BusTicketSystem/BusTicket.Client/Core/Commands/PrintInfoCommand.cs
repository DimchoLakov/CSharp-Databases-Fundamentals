using System;
using System.Text;
using BusTicket.Client.Core.Contracts;
using BusTicket.Services.Contracts;

namespace BusTicket.Client.Core.Commands
{
    public class PrintInfoCommand : IExecutable
    {
        private const string BusStationNotFound = "Bus station with id {0} not found!";

        private readonly IBusStationService _busStationService;

        public PrintInfoCommand(IBusStationService busStationService)
        {
            this._busStationService = busStationService;
        }

        public string Execute(string[] args)
        {
            int busStationId = int.Parse(args[0]);

            var busStation = this._busStationService.GetBusStationById(busStationId);

            if (busStation == null)
            {
                throw new InvalidOperationException(string.Format(BusStationNotFound, busStationId));
            }

            var sb = new StringBuilder();

            sb.AppendLine($"{busStation.Name}, {busStation.Town.Name}")
                .AppendLine("Arrivals:");

            foreach (var arrivalTrip in busStation.ArrivalTrips)
            {
                sb.AppendLine(
                    $"From {arrivalTrip.OriginBusStation.Name} | Arrive at: {arrivalTrip.ArrivalTime} | Status: {arrivalTrip.Status}");
            }

            sb.AppendLine("Departures:");

            foreach (var destTrip in busStation.DestionationTrips)
            {
                sb.AppendLine(
                    $"To {destTrip.DestinationBusStation.Name} | Depart at: {destTrip.DepartureTime} | Status {destTrip.Status}");
            }

            return sb.ToString().Trim();
        }
    }
}
