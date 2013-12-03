using Newtonsoft.Json;
using WinInstagram.Common;
using System;
namespace WinInstagram.InstagramAPI.Models
{
    public class Media : BindableBase
    {

        private string _type;
        [JsonProperty("type")]
        public string Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        private string _filter;
        [JsonProperty("filter")]
        public string Filter
        {
            get { return _filter; }
            set { SetProperty(ref _filter, value); }
        }

        private Likes _likes;
        [JsonProperty("likes")]
        public Likes Likes {
            get { return _likes; }
            set { SetProperty(ref _likes, value); } }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("created_time")]
        public string CreatedTime { get;
            set;
        }

        [JsonProperty("user")]
        public UserInfo User { get; set; }

        private Image _image;
        [JsonProperty("images")]
        public Image Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        private Comments _comments;
        [JsonProperty("comments")]
        public Comments Comments { get { return _comments; } set { SetProperty(ref _comments, value); } }

        [JsonProperty("caption")]
        public Caption Caption { get; set; }

        [JsonProperty("user_has_liked")]
        public bool UserHasLiked { get; set; }

        public override string ToString()
        {
            return Id;
        }
    }
}