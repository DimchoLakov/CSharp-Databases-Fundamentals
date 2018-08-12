using System;
using System.ComponentModel.DataAnnotations;

namespace BusTicket.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        public string Content { get; set; }

        [Range(1, 10)]
        public decimal Grade { get; set; }

        public int BusCompanyId { get; set; }
        public BusCompany BusCompany { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime DateOfPublishing { get; set; }
    }
}
