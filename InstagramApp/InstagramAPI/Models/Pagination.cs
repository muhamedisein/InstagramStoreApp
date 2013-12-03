using Newtonsoft.Json;

namespace WinInstagram.InstagramAPI.Models
{
    public class Pagination
    {
        [JsonProperty("next_url")]
        public string NextUrl { get; set; }

        [JsonProperty("next_max_id")]
        public string NextMaxId { get; set; }
    }
}