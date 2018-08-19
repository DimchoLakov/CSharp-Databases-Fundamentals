namespace CarDealer.App.DTOs.Export
{
    public class SaleDto
    {
        public CarSaleDto Car { get; set; }

        public string CustomerName { get; set; }

        public decimal Discount { get; set; }

        public decimal Price { get; set; }

        public decimal PriceWithDiscount { get; set; }
    }
}
