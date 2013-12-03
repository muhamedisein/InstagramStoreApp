using WinInstagram.InstagramAPI.Models;
using Newtonsoft.Json;

namespace WinInstagram.InstagramAPI.OAuth
{
    internal class OAuthResponse
    {
        public static OAuthResponse OAuthData = new OAuthResponse();


        [JsonProperty(PropertyName = "access_token")]
        public string AccesToken { get; protected set; }

        [JsonProperty(PropertyName = "user")]
        public UserInfo UserInfo { get; protected set; }
    }
}