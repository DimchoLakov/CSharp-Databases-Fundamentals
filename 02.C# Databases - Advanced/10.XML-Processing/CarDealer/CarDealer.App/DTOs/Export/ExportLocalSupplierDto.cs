using System.Xml.Serialization;

namespace CarDealer.App.DTOs.Export
{
    [XmlType("supplier")]
    public class ExportLocalSupplierDto
    {
        [XmlAttribute("id")]
        public int SupplierId { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("parts-count")]
        public int PartsCount { get; set; }
    }
}
