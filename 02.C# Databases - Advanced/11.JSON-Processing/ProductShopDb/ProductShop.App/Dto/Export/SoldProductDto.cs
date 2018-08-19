using Newtonsoft.Json;

namespace ProductShop.App.Dto.Export
{
    public class SoldProductDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
