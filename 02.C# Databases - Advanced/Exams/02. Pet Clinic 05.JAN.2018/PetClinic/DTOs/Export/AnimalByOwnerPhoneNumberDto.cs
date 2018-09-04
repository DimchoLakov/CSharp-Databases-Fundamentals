using System;
using Newtonsoft.Json;

namespace PetClinic.DTOs.Export
{
    public class AnimalByOwnerPhoneNumberDto
    {
        [JsonProperty("OwnerName")]
        public string OwnerName { get; set; }

        [JsonProperty("AnimalName")]
        public string AnimalName { get; set; }

        [JsonProperty("Age")]
        public int Age { get; set; }

        [JsonProperty("SerialNumber")]
        public string SerialNumber { get; set; }
        
        [JsonProperty("RegisteredOn")]
        public DateTime RegisteredOn { get; set; }
    }
}
