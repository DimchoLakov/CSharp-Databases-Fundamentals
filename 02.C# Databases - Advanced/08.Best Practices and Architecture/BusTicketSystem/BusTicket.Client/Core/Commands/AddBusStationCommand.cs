using System;
using BusTicket.Client.Core.Contracts;
using BusTicket.Services.Contracts;

namespace BusTicket.Client.Core.Commands
{
    public class AddBusStationCommand : IExecutable
    {
        private const string BusStationAlreadyExists = "Bus station {0} already exists!";
        private const string SuccessfullyAddedBusStation = "Bus station {0} successfully added.";

        private readonly IBusStationService _busStationService;

        public AddBusStationCommand(IBusStationService busStationService)
        {
            this._busStationService = busStationService;
        }

        public string Execute(string[] args)
        {
            string busStatioName = args[0];
            int townId = int.Parse(args[1]);

            var exists = this._busStationService.Exists(busStatioName);

            if (exists)
            {
                throw new InvalidOperationException(string.Format(BusStationAlreadyExists, busStatioName));
            }

            this._busStationService.AddBusStation(busStatioName, townId);

            return string.Format(SuccessfullyAddedBusStation, busStatioName);
        }
    }
}
