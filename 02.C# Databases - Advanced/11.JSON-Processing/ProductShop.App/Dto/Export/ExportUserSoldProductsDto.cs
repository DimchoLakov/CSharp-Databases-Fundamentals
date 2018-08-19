using Newtonsoft.Json;

namespace ProductShop.App.Dto.Export
{
    public class ExportUserSoldProductsDto
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("soldProducts")]
        public ExportSoldProductDto[] SoldProducts { get; set; }
    }
}
