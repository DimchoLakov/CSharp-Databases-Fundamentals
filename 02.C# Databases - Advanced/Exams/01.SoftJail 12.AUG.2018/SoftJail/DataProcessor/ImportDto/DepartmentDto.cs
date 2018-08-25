using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SoftJail.DataProcessor.ImportDto
{
    public class DepartmentDto
    {
        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 3)]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Cells")]
        public CellDto[] Cells { get; set; }
    }
}
