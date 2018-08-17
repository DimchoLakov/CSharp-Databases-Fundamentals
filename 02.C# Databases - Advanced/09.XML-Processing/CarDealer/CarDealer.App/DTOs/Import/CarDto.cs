using System.Xml.Serialization;

namespace CarDealer.App.DTOs.Import
{
    [XmlType("car")]
    public class CarDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("travelled-distance")]
        public double TravelledDistance { get; set; }
    }
}
