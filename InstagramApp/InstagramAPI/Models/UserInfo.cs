using Newtonsoft.Json;
using WinInstagram.Common;

namespace WinInstagram.InstagramAPI.Models
{
    public class UserInfo : BindableBase
    {
        public UserInfo()
        {

        }

        public UserInfo(string id, string userName, string fullName, string profilePicture)
        {
            Id = id;
            UserName = userName;
            FullName = fullName;
            ProfilePicture = profilePicture;
        }

        private string _id;
        [JsonProperty(PropertyName = "id")]
        public string Id { get { return _id; } set { SetProperty(ref _id, value); } }

        private string _userName;
        [JsonProperty(PropertyName = "username")]
        public string UserName { get { return _userName; } set { SetProperty(ref _userName, value); } }

        private string _fullName;
        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get { return _fullName; } set { SetProperty(ref _fullName, value); } }

        private string _profilePicture;
        [JsonProperty(PropertyName = "profile_picture")]
        public string ProfilePicture { get { return _profilePicture; } set { SetProperty(ref _profilePicture, value); } }
   }
}