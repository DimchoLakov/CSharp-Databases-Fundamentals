using System.Xml.Serialization;

namespace ProductShop.App.DTOs.Export
{
    [XmlType("category")]
    public class CategoryByProductsCountDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("products-count")]
        public int ProductsCount { get; set; }

        [XmlElement("average-price")]
        public decimal AveragePrice { get; set; }

        [XmlElement("total-revenue")]
        public decimal TotalRevenue { get; set; }
    }
}
