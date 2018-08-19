using System;
using Newtonsoft.Json;

namespace CarDealer.App.DTOs.Export
{
    public class CustomerDto
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("BirthDate")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("IsYoungDriver")]
        public bool IsYoungDriver { get; set; }

        [JsonProperty("Sales")]
        public int SalesCount { get; set; }
    }
}
