using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CarDealer.Models
{
    public class Car
    {
        public Car()
        {
            this.Sales = new HashSet<Sale>();
            this.CarParts = new List<CarPart>();
        }

        public int CarId { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public double TravelledDistance { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public virtual ICollection<CarPart> CarParts { get; set; }

        public decimal CarPrice
        {
            get { return this.CarParts.Select(cp => cp.Part.Price).Sum(); }
        }
    }
}
