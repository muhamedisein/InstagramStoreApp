using System;
using Newtonsoft.Json;
namespace WinInstagram.InstagramAPI.Models
{
    public class Caption
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        private string _createdTime;
        [JsonProperty("created_time")] 
        public string CreatedTime
        {
            get { return _createdTime; }
            set
            {
                double unixTime = Convert.ToDouble(value);
                var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                _createdTime = dtDateTime.AddSeconds(unixTime).ToLocalTime().ToString();
            }
        }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("from")]
        public UserInfo From { get; set; }
    }
}
