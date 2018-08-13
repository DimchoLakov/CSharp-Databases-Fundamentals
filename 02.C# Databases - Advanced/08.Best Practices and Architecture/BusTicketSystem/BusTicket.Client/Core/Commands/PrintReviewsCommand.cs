using System;
using BusTicket.Client.Core.Contracts;
using BusTicket.Services.Contracts;

namespace BusTicket.Client.Core.Commands
{
    public class PrintReviewsCommand : IExecutable
    {
        private const string BusCompanyDoesNotExist = "Bus company with id {0} does not exist!";
        private const string NoReviewsFound = "No reviews are found for company with id {0}";

        private readonly IBusCompanyService _busCompanyService;

        public PrintReviewsCommand(IBusCompanyService busCompanyService)
        {
            this._busCompanyService = busCompanyService;
        }

        public string Execute(string[] args)
        {
            int busCompanyId = int.Parse(args[0]);

            var busCompany = this._busCompanyService.GetBusCompanyById(busCompanyId);

            if (busCompany == null)
            {
                throw new InvalidOperationException(string.Format(BusCompanyDoesNotExist, busCompanyId));
            }

            var result = this._busCompanyService.PrintReviews(busCompanyId);

            result = result == string.Empty ? string.Format(NoReviewsFound, busCompanyId) : result;

            return result;
        }
    }
}
