using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace VaporStore.DataProcessor.ImportDto
{
    public class UserDto
    {
        [JsonProperty("Username")]
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Username { get; set; }

        [JsonProperty("FullName")]
        [RegularExpression(@"[A-Z]{1}[a-z]+\s{1}[A-Z]{1}[a-z]+")]
        public string FullName { get; set; }

        [JsonProperty("Email")]
        [Required]
        public string Email { get; set; }

        [JsonProperty("Age")]
        [Range(minimum: 3, maximum: 103)]
        public int Age { get; set; }

        public CardDto[] Cards { get; set; }
    }
}
