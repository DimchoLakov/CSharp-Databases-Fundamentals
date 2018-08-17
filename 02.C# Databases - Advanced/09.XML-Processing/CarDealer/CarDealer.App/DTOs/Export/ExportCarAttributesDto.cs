using System.Xml.Serialization;

namespace CarDealer.App.DTOs.Export
{
    [XmlType("car")]
    public class ExportCarAttributesDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public double TravelledDistance { get; set; }
    }
}
