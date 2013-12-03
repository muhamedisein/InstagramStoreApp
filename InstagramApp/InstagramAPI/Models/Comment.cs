using Newtonsoft.Json;
using WinInstagram.Common;
namespace WinInstagram.InstagramAPI.Models
{
    public class Comment : BindableBase
    {
        private string _id;
        [JsonProperty("id")]
        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _text;
        [JsonProperty("text")]
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        private UserInfo _from;
        [JsonProperty("from")]
        public UserInfo From
        {
            get { return _from; }
            set { SetProperty(ref _from, value); }
        }

        private string _createdTime;
        [JsonProperty("created_time")]
        public string CreatedTime
        {
            get { return _createdTime; }
            set { SetProperty(ref _createdTime, value); }
        }

        private string _caption;
        [JsonProperty("caption")]
        public string Caption
        {
            get { return _createdTime; }
            set { SetProperty(ref _caption, value); }
        }
    }
}