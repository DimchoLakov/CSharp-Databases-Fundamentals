using System.Xml.Serialization;

namespace ProductShop.App.DTOs.Export
{
    [XmlType("product")]
    public class SoldProductAttributesDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }

    }
}
