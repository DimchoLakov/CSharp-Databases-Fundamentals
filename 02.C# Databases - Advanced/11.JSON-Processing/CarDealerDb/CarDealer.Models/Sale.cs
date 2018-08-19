using System.Collections.Generic;

namespace CarDealer.Models
{
    public class Sale
    {
        public int SaleId { get; set; }

        public decimal Discount { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
