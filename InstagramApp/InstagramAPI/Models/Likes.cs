using System.Collections.ObjectModel;
using Newtonsoft.Json;
using WinInstagram.Common;

namespace WinInstagram.InstagramAPI.Models
{
    public class Likes : BindableBase
    {
        private int _count;
        [JsonProperty("count")]
        public int Count
        {
            get { return _count; }
            set { SetProperty(ref _count, value); }
        }


        private ObservableCollection<UserInfo> _data;
        [JsonProperty("data")]
        public ObservableCollection<UserInfo> Data
        {
            get { return _data; }
            set { SetProperty(ref _data, value); }
        }
    }
}