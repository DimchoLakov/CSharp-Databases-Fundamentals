using Newtonsoft.Json;

namespace ProductShop.App.Dto.Import
{
    public class ImportCategoryDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
