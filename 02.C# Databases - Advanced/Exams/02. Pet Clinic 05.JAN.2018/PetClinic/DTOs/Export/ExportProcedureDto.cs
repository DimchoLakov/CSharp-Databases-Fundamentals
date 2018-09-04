using System.Xml.Serialization;

namespace PetClinic.DTOs.Export
{
    [XmlType("Procedure")]
    public class ExportProcedureDto
    {
        [XmlElement("Passport")]
        public string PassportSerialNumber { get; set; }

        [XmlElement("OwnerNumber")]
        public string OwnerNumber { get; set; }

        [XmlElement("DateTime")]
        public string DateTime { get; set; }

        [XmlArray("AnimalAids")]
        public ExportProcedureAnimalAidDto[] AnimalAids { get; set; }

        [XmlElement("TotalPrice")]
        public decimal TotalPrice { get; set; }
    }
}
