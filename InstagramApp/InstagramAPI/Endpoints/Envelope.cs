using WinInstagram.InstagramAPI.Models;
using Newtonsoft.Json;

namespace WinInstagram.InstagramAPI.Endpoints
{
    /// <summary>
    /// Intagram responose wrapper
    /// </summary>
    /// <typeparam name="T">
    ///  Generic data to be stored like Media, User, Comments, etc.
    /// </typeparam>
    public class Envelope<T>
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}