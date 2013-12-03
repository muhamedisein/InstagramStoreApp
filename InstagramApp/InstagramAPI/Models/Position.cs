using Newtonsoft.Json;

namespace WinInstagram.InstagramAPI.Models
{
    public class Position
    {
        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }
    }
}