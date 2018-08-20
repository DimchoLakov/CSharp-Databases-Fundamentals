using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    public class MessageDto
    {
        [XmlElement("Description")]
        public string Description { get; set; }
    }
}
