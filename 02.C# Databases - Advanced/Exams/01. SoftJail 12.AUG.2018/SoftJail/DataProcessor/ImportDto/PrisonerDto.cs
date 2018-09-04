using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SoftJail.DataProcessor.ImportDto
{
    public class PrisonerDto
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression("^The [A-Z]{1}[a-zA-Z]+$")]
        [JsonProperty("Nickname")]
        public string Nickname { get; set; }

        [Required]
        [Range(minimum: 18, maximum: 65)]
        [JsonProperty("Age")]
        public int Age { get; set; }

        [Required]
        [JsonProperty("IncarcerationDate")]
        public DateTime IncarcerationDate { get; set; }
        
        [JsonProperty("ReleaseDate")]
        public DateTime? ReleaseDate { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        [JsonProperty("Bail")]
        public decimal? Bail { get; set; }

        [JsonProperty("CellId")]
        public int? CellId { get; set; }
        
        [JsonProperty("Mails")]
        public MailDto[] Mails { get; set; }
    }
}
