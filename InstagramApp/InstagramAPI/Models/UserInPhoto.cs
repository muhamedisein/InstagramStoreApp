using Newtonsoft.Json;

namespace WinInstagram.InstagramAPI.Models
{
    public class UserInPhoto
    {
        [JsonProperty("user")]
        public UserInfo User { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }
    }
}