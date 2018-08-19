using Newtonsoft.Json;

namespace CarDealer.App.DTOs.Import
{
    public class ImportCarDto
    {
        [JsonProperty("make")]
        public string Make { get; set; }
        
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("travelledDistance")]
        public double TravelledDistance { get; set; }
    }
}
