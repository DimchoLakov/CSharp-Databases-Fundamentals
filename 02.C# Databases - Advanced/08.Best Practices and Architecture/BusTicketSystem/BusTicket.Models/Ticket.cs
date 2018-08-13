namespace BusTicket.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public decimal Price { get; set; }

        public int Seat { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
