using BusTicket.Data;
using BusTicket.Models;
using BusTicket.Services.Contracts;namespace BusTicket.Services
{
    public class TicketService : ITicketService
    {
        private readonly BusTicketContext _dbContext;
        private readonly ITripService _tripService;
        private readonly ICustomerService _customerService;

        public TicketService(BusTicketContext dbContext, ITripService tripService, ICustomerService customerService)
        {
            this._dbContext = dbContext;
            this._tripService = tripService;
            this._customerService = customerService;
        }

        public string BuyTicket(int customerId, int tripId, decimal price, int seat)
        {
            var customer = this._customerService
                .GetCustomerById(customerId);

            var trip = this._tripService
                .GetTripById(tripId);

            var ticket = new Ticket()
            {
                Customer =  customer,
                Trip = trip,
                Price = price,
                Seat = seat
            };

            this._dbContext
                .Tickets
                .Add(ticket);

            this._dbContext
                .SaveChanges();

            string result =
                $"Customer {customer.FirstName} {customer.LastName} bought ticket for trip {tripId} for {price} on seat {seat}";

            return result;
        }
    }
}
