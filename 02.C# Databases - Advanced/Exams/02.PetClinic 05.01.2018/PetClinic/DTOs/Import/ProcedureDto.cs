using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace PetClinic.DTOs.Import
{
    [XmlType("Procedure")]
    public class ProcedureDto
    {
        [Required]
        [XmlElement("Vet")]
        [StringLength(maximumLength: 40, MinimumLength = 3)]
        public string Vet { get; set; }

        [Required]
        [XmlElement("Animal")]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string SerialNumber { get; set; }

        [Required]
        [XmlElement("DateTime")]
        public string DateTime { get; set; }
        
        [XmlArray("AnimalAids")]
        public ProcedureAnimalAidDto[] AnimalAids { get; set; }
    }
}
