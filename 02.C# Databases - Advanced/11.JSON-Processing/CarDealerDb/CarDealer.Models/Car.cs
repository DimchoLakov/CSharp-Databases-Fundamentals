using System.Collections.Generic;
using System.Linq;

namespace CarDealer.Models
{
    public class Car
    {
        public Car()
        {
            this.Sales = new List<Sale>();
            this.PartCars = new List<PartCar>();
        }

        public int CarId { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public double TravelledDistance { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public ICollection<PartCar> PartCars { get; set; }

        public decimal CarPrice => this.PartCars.Sum(x => x.Part.Price);
    }
}
