using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PetClinic.DTOs.Import
{
    public class AnimalDto
    {
        [Required]
        [JsonProperty("Name")]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [JsonProperty("Type")]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Type { get; set; }

        [Required]
        [JsonProperty("Age")]
        [Range(1, 500)]
        public int Age { get; set; }

        [Required]
        [JsonProperty("Passport")]
        public PassportDto Passport { get; set; }
    }
}
