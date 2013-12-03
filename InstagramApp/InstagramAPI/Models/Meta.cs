using System;
using Newtonsoft.Json;

namespace WinInstagram.InstagramAPI.Models
{
    public class Meta
    {
        public Meta()
        {
            ErrorType = Code = Message = "";
        }

        public Meta(string errorType, string code, String message)
        {
            ErrorType = errorType;
            Code = code;
            Message = message;
        }

        [JsonProperty("error_type")]
        public string ErrorType { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("error_message")]
        public string Message { get; set; }

       
    }
}