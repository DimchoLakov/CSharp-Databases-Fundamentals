using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace PetClinic.DTOs.Import
{
    [XmlType("AnimalAid")]
    public class ProcedureAnimalAidDto
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        [XmlElement("Name")]
        public string Name { get; set; }
    }
}
