namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Dtos;
    using Contracts;
    using Services.Contracts;

    public class AddTownCommand : ICommand
    {
        private const string TownAlreadyExists = "Town {0} was already added!";
        private const string InvalidCredentials = "Invalid credentials!";
        private const string SuccessfullyAddedTown = "Town {0} was added successfully!";

        private readonly ITownService _townService;
        private readonly IUserSessionService _userSessionService;

        public AddTownCommand(ITownService townService, IUserSessionService userSessionService)
        {
            this._townService = townService;
            this._userSessionService = userSessionService;
        }

        // AddTown <townName> <countryName>
        public string Execute(string[] data)
        {
            var isLoggedIn = this._userSessionService.IsLoggedIn;

            if (!isLoggedIn)
            {
                throw new InvalidOperationException(InvalidCredentials);
            }

            string townName = data[0];
            string countryName = data[1];

            var townExists = this._townService.Exists(townName);

            if (townExists)
            {
                throw new ArgumentException(string.Format(TownAlreadyExists, townName));
            }
            
            this._townService.Add(townName, countryName);

            return string.Format(SuccessfullyAddedTown, townName);
        }
    }
}
