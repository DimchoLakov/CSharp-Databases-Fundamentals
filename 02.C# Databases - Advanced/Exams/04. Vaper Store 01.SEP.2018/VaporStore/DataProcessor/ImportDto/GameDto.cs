using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor.ImportDto
{
    public class GameDto
    {
        [JsonProperty("Name")]
        [Required]
        public string Name { get; set; }

        [JsonProperty("Price")]
        [Required]
        [Range(minimum: 0, maximum: double.MaxValue)]
        public decimal Price { get; set; }
        
        [JsonProperty("ReleaseDate")]
        [Required]
        public string ReleaseDate { get; set; }

        [JsonProperty("Developer")]
        [Required]
        public string DeveloperName { get; set; }

        [JsonProperty("Genre")]
        [Required]
        public string GenreName { get; set; }
        
        [JsonProperty("Tags")]
        public string[] Tags { get; set; }
    }
}
