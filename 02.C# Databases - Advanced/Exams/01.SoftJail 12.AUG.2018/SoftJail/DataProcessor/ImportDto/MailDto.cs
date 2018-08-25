using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SoftJail.DataProcessor.ImportDto
{
    public class MailDto
    {
        [Required]
        [JsonProperty("Description")]
        public string Description { get; set; }

        [Required]
        [JsonProperty("Sender")]
        public string Sender { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]+ str\\.$")]
        [JsonProperty("Address")]
        public string Address { get; set; }
    }
}
