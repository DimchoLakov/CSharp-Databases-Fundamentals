using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.ImportDto
{
    [XmlType("Purchase")]
    public class PurchaseDto
    {
        [Required]
        [XmlAttribute("title")]
        public string Title { get; set; }

        [Required]
        [XmlElement("Type")]
        public string Type { get; set; }

        [Required]
        [XmlElement("Key")]
        [RegularExpression(@"[A-Z0-9]{4}\-[A-Z0-9]{4}\-[A-Z0-9]{4}")]
        public string Key { get; set; }

        [Required]
        [XmlElement("Card")]
        [RegularExpression(@"\d{4}\s{1}\d{4}\s{1}\d{4}\s{1}\d{4}")]
        public string Card { get; set; }

        [Required]
        [XmlElement("Date")]
        public string Date { get; set; }
    }
}
