using System.Xml.Serialization;

namespace CarDealer.App.DTOs.Export
{
    [XmlType("car")]
    public class ExportCarFerrariDto
    {
        [XmlAttribute("id")]
        public int CarId { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public double TravelledDistance { get; set; }
    }
}
