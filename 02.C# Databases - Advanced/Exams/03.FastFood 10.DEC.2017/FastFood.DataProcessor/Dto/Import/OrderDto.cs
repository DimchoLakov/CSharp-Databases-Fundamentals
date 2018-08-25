using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace FastFood.DataProcessor.Dto.Import
{
    [XmlType("Order")]
    public class OrderDto
    {
        [XmlElement("Customer")]
        [Required]
        public string Customer { get; set; }

        [XmlElement("Employee")]
        [MinLength(3), MaxLength(30)]
        [Required]
        public string Employee { get; set; }

        [XmlElement("DateTime")]
        [Required]
        public string DateTime { get; set; }

        [XmlElement("Type")]
        [Required]
        public string Type { get; set; }

        [XmlArray("Items")]
        public OrderItemDto[] Items { get; set; }
    }
}
