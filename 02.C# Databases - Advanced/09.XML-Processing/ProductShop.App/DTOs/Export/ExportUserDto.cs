using System.Xml.Serialization;

namespace ProductShop.App.DTOs.Export
{
    [XmlType("user")]
    public class ExportUserDto
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }
        
        [XmlArray("sold-products")]
        public SoldProductDto[] SoldProducts { get; set; }
    }
}
