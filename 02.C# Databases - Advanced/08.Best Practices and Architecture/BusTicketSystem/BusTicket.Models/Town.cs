using System.Collections.Generic;

namespace BusTicket.Models
{
    public class Town
    {
        public Town()
        {
            this.BusStations = new HashSet<BusStation>();
            this.Customers = new HashSet<Customer>();
        }

        public Town(string name, string country) : this()
        {
            this.Name = name;
            this.Country = country;
        }

        public int TownId { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public ICollection<BusStation> BusStations { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
