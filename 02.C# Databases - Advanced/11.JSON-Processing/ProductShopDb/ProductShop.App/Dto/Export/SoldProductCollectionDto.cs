using Newtonsoft.Json;

namespace ProductShop.App.Dto.Export
{
    public class SoldProductCollectionDto
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("products")]
        public SoldProductDto[] Products { get; set; }
    }
}
