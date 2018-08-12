using System;
using BusTicket.Client.Core.Contracts;
using BusTicket.Services.Contracts;

namespace BusTicket.Client.Core.Commands
{
    public class AddTownCommand : IExecutable
    {
        private const string TownAlreadyExists = "Town {0} already exists";
        private const string SuccessfullyAddedTown = "Successfully added town {0}";

        private readonly ITownService _townService;

        public AddTownCommand(ITownService townService)
        {
            this._townService = townService;
        }

        public string Execute(string[] args)
        {
            string townName = args[0];
            string countryName = args[1];

            var townExists = this._townService.Exists(townName);

            if (townExists)
            {
                throw new InvalidOperationException(string.Format(TownAlreadyExists, townName));
            }

            this._townService.AddTown(townName, countryName);

            return string.Format(SuccessfullyAddedTown, townName);
        }
    }
}
