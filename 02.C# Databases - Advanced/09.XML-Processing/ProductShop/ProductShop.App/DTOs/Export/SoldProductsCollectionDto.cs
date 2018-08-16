using System.Xml.Serialization;

namespace ProductShop.App.DTOs.Export
{
    [XmlType("sold-products")]
    public class SoldProductsCollectionDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("products")]
        public SoldProductAttributesDto[] SoldProductDtos { get; set; }
    }
}
