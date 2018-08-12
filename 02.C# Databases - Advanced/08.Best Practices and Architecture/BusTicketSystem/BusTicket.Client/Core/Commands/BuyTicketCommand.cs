using System;
using BusTicket.Client.Core.Contracts;
using BusTicket.Services.Contracts;

namespace BusTicket.Client.Core.Commands
{
    public class BuyTicketCommand : IExecutable
    {
        private const string CustomerNotFound = "Customer not found!";
        private const string TripNotFound = "Trip not found!";

        private readonly ICustomerService _customerService;
        private readonly ITripService _tripService;
        private readonly ITicketService _ticketService;


        public BuyTicketCommand(ICustomerService customerService, ITripService tripService, ITicketService ticketService)
        {
            this._customerService = customerService;
            this._tripService = tripService;
            this._ticketService = ticketService;
        }

        public string Execute(string[] args)
        {
            var customerId = int.Parse(args[0]);
            var tripId = int.Parse(args[1]);
            var price = decimal.Parse(args[2]);
            var seat = int.Parse(args[3]);

            var customerExists = this._customerService.Exists(customerId);
            var tripExists = this._tripService.Exists(tripId);

            if (!customerExists)
            {
                throw new InvalidOperationException(CustomerNotFound);
            }

            if (!tripExists)
            {
                throw new InvalidOperationException(TripNotFound);
            }

            var result = this._ticketService.BuyTicket(customerId, tripId, price, seat);

            return result;
        }
    }
}
