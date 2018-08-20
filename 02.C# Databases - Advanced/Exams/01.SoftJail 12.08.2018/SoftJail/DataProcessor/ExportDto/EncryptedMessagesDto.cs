using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    public class EncryptedMessagesDto
    {
        [XmlElement("Message")]
        public MessageDto[] Messages { get; set; }
    }
}
