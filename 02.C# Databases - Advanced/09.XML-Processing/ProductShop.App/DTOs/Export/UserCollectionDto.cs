using System.Xml.Serialization;

namespace ProductShop.App.DTOs.Export
{
    [XmlRoot("users")]
    public class UserCollectionDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("user")]
        public UserDto[] UserDtos { get; set; }
    }
}
