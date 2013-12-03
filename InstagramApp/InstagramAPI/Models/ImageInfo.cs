using Newtonsoft.Json;
using WinInstagram.Common;
namespace WinInstagram.InstagramAPI.Models
{
    public class ImageInfo : BindableBase
    {
        private string _url;

        [JsonProperty("url")]
        public string Url
        {
            get { return _url; }
            set { SetProperty(ref _url, value); }
        }

        private int _width;
        [JsonProperty("width")]
        public int Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }

        private int _height;
        [JsonProperty("height")]
        public int Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }
    }
}