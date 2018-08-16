using System.Xml.Serialization;

namespace ProductShop.App.DTOs.Export
{
    [XmlType("product")]
    public class ProductInRangeDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("buyer")]
        public string BuyerFullName { get; set; }
    }
}
