using System.Xml.Serialization;

namespace CarDealer.App.DTOs.Export
{
    [XmlType("sale")]
    public class ExportSaleDiscountDto
    {
        [XmlElement("car")]
        public ExportCarAttributesDto Car { get; set; }

        [XmlElement("customer-name")]
        public string CustomerName { get; set; }

        [XmlElement("discount")]
        public decimal Discount { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("price-with-discount")]
        public string PriceWithDiscount { get; set; }
        //public decimal PriceWithDiscount { get; set; }
    }
}
