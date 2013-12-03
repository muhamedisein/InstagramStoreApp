using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WinInstagram.Common;

namespace WinInstagram.InstagramAPI.Models
{
    public class Comments:BindableBase
    {
        private int _count;
        [JsonProperty("count")]
        public int Count
        {
            get { return _count; }
            set { SetProperty(ref _count, value); }
        }

        private ObservableCollection<Comment> _commmentsList;
        [JsonProperty("data")]
        public ObservableCollection<Comment> CommentsList
        {
            get { return _commmentsList; }
            set { SetProperty(ref _commmentsList, value); }
        }

      
    }
}
