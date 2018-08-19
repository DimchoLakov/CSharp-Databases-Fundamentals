﻿using Newtonsoft.Json;

namespace ProductShop.App.Dto.Export
{
    public class UserDto
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("age")]
        public int? Age { get; set; }

        [JsonProperty("soldProducts")]
        public SoldProductCollectionDto SoldProducts { get; set; }
    }
}
