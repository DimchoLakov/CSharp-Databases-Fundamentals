namespace BusTicket.Services.Contracts
{
    public interface ITicketService
    {
        string BuyTicket(int customerId, int tripId, decimal price, int seat);
    }
}
