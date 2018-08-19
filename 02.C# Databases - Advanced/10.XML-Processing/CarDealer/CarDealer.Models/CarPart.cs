namespace CarDealer.Models
{
    public class CarPart
    {
        public int CarId { get; set; }
        public virtual Car Car { get; set; }

        public int PartId { get; set; }
        public virtual Part Part { get; set; }
    }
}
