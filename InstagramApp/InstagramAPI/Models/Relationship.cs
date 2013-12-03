using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WinInstagram.InstagramAPI.Models
{
    class Relationship
    {
        [JsonProperty("outgoing_status")]
        public string OutgoingStatus { get; set; }

        [JsonProperty("incoming_status")]
        public string IncomingStatus { get; set; }
    }
}
