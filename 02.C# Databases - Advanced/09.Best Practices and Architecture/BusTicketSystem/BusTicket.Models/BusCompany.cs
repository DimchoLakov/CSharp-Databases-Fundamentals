using System;
using System.Collections.Generic;

namespace BusTicket.Models
{
    public class BusCompany
    {
        public BusCompany()
        {
            this.Reviews = new HashSet<Review>();
            this.Trips = new List<Trip>();
        }

        public int BusCompanyId { get; set; }

        public string Name { get; set; }
        
        public string Nationality { get; set; }

        public string Rating { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
