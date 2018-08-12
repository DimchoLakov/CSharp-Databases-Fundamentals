using System;
using BusTicket.Client.Core.Contracts;
using BusTicket.Services.Contracts;

namespace BusTicket.Client.Core.Commands
{
    public class AddBusCompanyCommand : IExecutable
    {
        private const string SuccessfullyCreatedBusCompany = "Bus company {0} successfully created!";
        private const string BusCompanyAlreadyExists = "Bus company {0} already exists";

        private readonly IBusCompanyService _busCompanyService;

        public AddBusCompanyCommand(IBusCompanyService busCompanyService)
        {
            this._busCompanyService = busCompanyService;
        }

        public string Execute(string[] args)
        {
            string name = args[0];
            string nationality = args[1];

            var busCompanyExists = this._busCompanyService.Exists(name);

            if (busCompanyExists)
            {
                throw new InvalidOperationException(string.Format(BusCompanyAlreadyExists, name));
            }

            this._busCompanyService.CreateBusCompany(name, nationality);

            return string.Format(SuccessfullyCreatedBusCompany, name);
        }
    }
}
