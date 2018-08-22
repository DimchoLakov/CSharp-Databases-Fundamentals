using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PetClinic.DTOs.Import
{
    public class AnimalAidDto
    {
        [Required]
        [JsonProperty("Name")]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [JsonProperty("Price")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }
    }
}
