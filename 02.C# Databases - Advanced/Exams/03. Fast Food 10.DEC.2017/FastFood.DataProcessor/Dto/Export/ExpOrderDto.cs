using Newtonsoft.Json;

namespace FastFood.DataProcessor.Dto.Export
{
    public class ExpOrderDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Orders")]
        public ExpOrderItemDto[] Orders { get; set; }

        [JsonProperty("TotalMade")]
        public decimal TotalMade { get; set; }
    }
}
