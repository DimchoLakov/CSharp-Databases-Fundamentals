using Newtonsoft.Json;

namespace ProductShop.App.Dto.Import
{
    public class ImportProductDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
