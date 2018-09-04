using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SoftJail.DataProcessor.ImportDto
{
    public class CellDto
    {
        [Required]
        [Range(1, 1000)]
        [JsonProperty("CellNumber")]
        public int CellNumber { get; set; }
        
        [Required]
        [JsonProperty("HasWindow")]
        public bool HasWindow { get; set; }
    }
}
