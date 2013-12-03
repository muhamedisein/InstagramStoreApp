using Newtonsoft.Json;

namespace WinInstagram.InstagramAPI.Models
{
    public class Counts
    {
        public Counts()
        {
            Media = Follows = FollowedBy = 0;
        }

        public Counts(int media, int follows, int followedBy)
        {
            Media = media;
            Follows = follows;
            FollowedBy = followedBy;
        }

        [JsonProperty("media")]
        public int Media { get; set; }

        [JsonProperty("follows")]
        public int Follows { get; set; }

        [JsonProperty("followed_by")]
        public int FollowedBy { get; set; }
    }
}