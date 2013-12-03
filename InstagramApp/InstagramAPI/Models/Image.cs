using Newtonsoft.Json;
using WinInstagram.Common;
namespace WinInstagram.InstagramAPI.Models
{
    public class Image : BindableBase
    {
        private ImageInfo _lowResolution;

        [JsonProperty("low_resolution")]
        public ImageInfo LowResolution
        {
            get { return _lowResolution; }
            set { SetProperty(ref _lowResolution, value); }
        }

        private ImageInfo _thumbnail;
        [JsonProperty("thumbnail")]
        public ImageInfo Thumbnail
        {
            get { return _thumbnail; }
            set { SetProperty(ref _thumbnail, value); }
        }

        private ImageInfo _standardRsolution;
        [JsonProperty("standard_resolution")]
        public ImageInfo StandardResolution
        {
            get { return _standardRsolution; }
            set { SetProperty(ref _standardRsolution, value); }
        }
    }
}