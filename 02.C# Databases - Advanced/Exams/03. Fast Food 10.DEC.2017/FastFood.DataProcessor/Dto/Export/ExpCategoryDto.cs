using System.Xml.Serialization;

namespace FastFood.DataProcessor.Dto.Export
{
    [XmlType("Category")]
    public class ExpCategoryDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("MostPopularItem")]
        public ExpMostPopularItemDto MostPopularItem { get; set; }
    }
}
