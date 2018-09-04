using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace PetClinic.DTOs.Import
{
    [XmlType("Vet")]
    public class VetDto
    {
        [Required]
        [XmlElement("Name")]
        [StringLength(maximumLength: 40, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [XmlElement("Profession")]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Profession { get; set; }

        [Required]
        [XmlElement("Age")]
        [Range(minimum: 22, maximum: 65)]
        public int Age { get; set; }

        [Required]
        [XmlElement("PhoneNumber")]
        [RegularExpression(@"(\+359\d{9})|(0\d{9})")]
        public string PhoneNumber { get; set; }
    }
}
