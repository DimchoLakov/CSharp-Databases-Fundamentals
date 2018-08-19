using Newtonsoft.Json;

namespace CarDealer.App.DTOs.Export
{
    public class CustomerSaleDto
    {
        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("boughtCars")]
        public int BoughtCarsCount { get; set; }

        [JsonProperty("spentMoney")]
        public string SpentMoney { get; set; }
    }
}
