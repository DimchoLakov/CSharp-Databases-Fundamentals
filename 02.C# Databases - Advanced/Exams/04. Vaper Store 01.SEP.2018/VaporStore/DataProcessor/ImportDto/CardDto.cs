using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using VaporStore.Data.Models.Enums;

namespace VaporStore.DataProcessor.ImportDto
{
    public class CardDto
    {
        [JsonProperty("Number")]
        [Required]
        [RegularExpression(@"\d{4}\s{1}\d{4}\s{1}\d{4}\s{1}\d{4}")]
        public string Number { get; set; }

        [JsonProperty("CVC")]
        [Required]
        [RegularExpression(@"\d{3}")]
        public string Cvc { get; set; }

        [JsonProperty("Type")]
        [Required]
        public string Type { get; set; }
    }
}
