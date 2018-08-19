using Newtonsoft.Json;

namespace CarDealer.App.DTOs.Export
{
    public class PartDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Price")]
        public decimal Price { get; set; }
    }
}
