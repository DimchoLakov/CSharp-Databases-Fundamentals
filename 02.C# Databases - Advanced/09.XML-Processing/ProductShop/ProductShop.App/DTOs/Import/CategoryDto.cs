using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ProductShop.App.DTOs.Import
{
    [XmlType("category")]
    public class CategoryDto
    {
        [XmlElement("name")]
        [StringLength(maximumLength: 15, MinimumLength = 3, ErrorMessage = "Category name must have min 3 and max 15 chars!")]
        public string Name { get; set; }
    }
}
