using Newtonsoft.Json;

namespace CarDealer.App.DTOs.Import
{
    public class ImportPartDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
