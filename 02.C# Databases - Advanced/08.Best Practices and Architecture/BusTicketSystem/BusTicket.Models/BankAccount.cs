namespace BusTicket.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }

        public int AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
