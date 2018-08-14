using System;
using BusTicket.Client.Core.Contracts;
using BusTicket.Models.Enums;
using BusTicket.Services.Contracts;

namespace BusTicket.Client.Core.Commands
{
    public class AddCustomerCommand : IExecutable
    {
        private const string InvalidInput = "Invalid input!";
        private const string SuccessfullyAddedCustomer = "Customer {0} added successfully!";

        private readonly ICustomerService _customerService;
        private readonly IBusStationService _busStationService;

        public AddCustomerCommand(ICustomerService customerService, IBusStationService busStationService)
        {
            this._customerService = customerService;
            this._busStationService = busStationService;
        }

        public string Execute(string[] args)
        {
            string firstName = args[0];
            string lastName = args[1];
            string gender = args[2];

            if (!Enum.TryParse(gender, true, out Gender gen))
            {
                throw new ArgumentException(InvalidInput);
            }

            this._customerService.AddCustomer(firstName, lastName, gen);

            return string.Format(SuccessfullyAddedCustomer, firstName);
        }
    }
}
