using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace FastFood.DataProcessor.Dto.Import
{
    [XmlType("Item")]
    public class OrderItemDto
    {
        [XmlElement("Name")]
        [MinLength(3), MaxLength(30)]
        [Required]
        public string ItemName { get; set; }

        [XmlElement("Quantity")]
        public int Quantity { get; set; }
    }
}
