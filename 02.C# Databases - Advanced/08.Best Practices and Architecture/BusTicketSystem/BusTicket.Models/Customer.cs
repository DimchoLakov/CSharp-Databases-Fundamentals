using System;
using System.Collections.Generic;
using BusTicket.Models.Enums;

namespace BusTicket.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Tickets = new HashSet<Ticket>();

            this.Reviews = new HashSet<Review>();
        }

        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public int? HomeTownId { get; set; }
        public virtual Town HomeTown { get; set; }

        public int? BankAccountId { get; set; }
        public virtual BankAccount BankAccount { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
