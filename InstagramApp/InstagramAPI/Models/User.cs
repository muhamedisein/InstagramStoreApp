using Newtonsoft.Json;

namespace WinInstagram.InstagramAPI.Models
{
    public class User : UserInfo
    {
        public User()
        {
        }

        public User(string id, string userName, string fullName,
                    string profilePicture, string bio,
                    string webSite, Counts counts) : base(id, userName, fullName, profilePicture)
        {
            {
                Bio = bio;
                WebSite = webSite;
                Counts = counts;
            }
        }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("website")]
        public string WebSite { get; set; }

        [JsonProperty("counts")]
        public Counts Counts { get; set; }
    }
}