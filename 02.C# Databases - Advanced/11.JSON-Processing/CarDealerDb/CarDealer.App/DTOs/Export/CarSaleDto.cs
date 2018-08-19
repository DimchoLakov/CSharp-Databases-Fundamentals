using Newtonsoft.Json;

namespace CarDealer.App.DTOs.Export
{
    public class CarSaleDto
    {
        [JsonProperty("Make")]
        public string Make { get; set; }

        [JsonProperty("Model")]
        public string Model { get; set; }

        [JsonProperty("TravelledDistance")]
        public double TravelledDistance { get; set; }
    }
}
