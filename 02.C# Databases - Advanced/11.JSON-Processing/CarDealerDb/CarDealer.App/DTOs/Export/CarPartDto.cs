using Newtonsoft.Json;

namespace CarDealer.App.DTOs.Export
{
    [JsonObject("car")]
    public class CarPartDto
    {
        [JsonProperty("Make")]
        public string Make { get; set; }

        [JsonProperty("Model")]
        public string Model { get; set; }

        [JsonProperty("TravelledDistance")]
        public double TravelledDistance { get; set; }

        [JsonProperty("parts")]
        public PartDto[] Parts { get; set; }
    }
}
