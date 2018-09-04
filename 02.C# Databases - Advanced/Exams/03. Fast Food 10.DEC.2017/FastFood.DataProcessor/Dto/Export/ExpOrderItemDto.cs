using Newtonsoft.Json;

namespace FastFood.DataProcessor.Dto.Export
{
    public class ExpOrderItemDto
    {
        [JsonProperty("Customer")]
        public string Customer { get; set; }

        [JsonProperty("Items")]
        public ExpItemDto[] Items { get; set; }

        [JsonProperty("TotalPrice")]
        public decimal TotalPrice { get; set; }
    }
}
