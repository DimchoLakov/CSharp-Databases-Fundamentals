using FastFood.Models;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FastFood.DataProcessor.Dto.Import
{
    public class EmployeeDto
    {
        [JsonProperty("Name")]
        [MinLength(3), MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [JsonProperty("Age")]
        [Range(15, 80)]
        [Required]
        public int Age { get; set; }

        [JsonProperty("Position")]
        [MinLength(3), MaxLength(30)]
        [Required]
        public string Position { get; set; }
    }
}
