using Newtonsoft.Json;

namespace ProductShop.App.Dto.Export
{
    public class UserCollectionDto
    {
        [JsonProperty("usersCount")]
        public int Count { get; set; }

        [JsonProperty("users")]
        public UserDto[] Users { get; set; }
    }
}
