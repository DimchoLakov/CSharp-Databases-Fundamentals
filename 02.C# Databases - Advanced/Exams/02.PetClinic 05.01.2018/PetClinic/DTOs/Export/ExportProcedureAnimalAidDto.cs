using System.Xml.Serialization;

namespace PetClinic.DTOs.Export
{
    [XmlType("AnimalAid")]
    public class ExportProcedureAnimalAidDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Price")]
        public decimal Price { get; set; }
    }
}
