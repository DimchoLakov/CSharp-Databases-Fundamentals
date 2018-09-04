using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PetClinic.DTOs.Import
{
    public class PassportDto
    {
        [Required]
        [JsonProperty("SerialNumber")]
        [RegularExpression(@"[a-zA-Z]{7}\d{3}")]
        public string SerialNumber { get; set; }

        [Required]
        [JsonProperty("OwnerName")]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string OwnerName { get; set; }

        [Required]
        [JsonProperty("OwnerPhoneNumber")]
        [RegularExpression(@"(\+359\d{9})|(0\d{9})")]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        [JsonProperty("RegistrationDate")]
        public DateTime RegistrationDate { get; set; }
    }
}
