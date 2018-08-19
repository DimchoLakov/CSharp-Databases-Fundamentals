using Newtonsoft.Json;

namespace CarDealer.App.DTOs.Export
{
    public class SupplierDto
    {
        [JsonProperty("ID")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("PartsCount")]
        public int PartsCount { get; set; }
    }
}
