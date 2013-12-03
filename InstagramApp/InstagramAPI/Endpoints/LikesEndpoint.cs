using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinInstagram.InstagramAPI.Models;
using WinInstagram.InstagramAPI.OAuth;
using WinInstagram.Utilites.Web;
using Newtonsoft.Json;

namespace WinInstagram.InstagramAPI.Endpoints
{
    /// <summary>
    ///  Likes Endpoint for Instagram API to get list of likes of media or like/unlike a media.
    /// </summary>
    class LikesEndpoint : Endpoint
    {
        protected static string Section { get; set; }

        static LikesEndpoint()
        {
            Section = "/media/";
        }

        public static async Task<ObservableCollection<UserInfo>> Likes(string mediaId)
        {
            string url = String.Format("{0}{1}{2}/likes", ApiUrl, Section, mediaId);

            string result = await HttpUtilities.Get(url);

            var jsonObj = await JsonConvert.DeserializeObjectAsync<Envelope<ObservableCollection<UserInfo>>>(result);

            if (jsonObj.Meta.Code.Equals("200"))
            {
                return jsonObj.Data;
            }
            throw new Exception(jsonObj.Meta.Message);

        }

        public static async Task<bool> Like(OAuthResponse oAuth, string mediaId)
        {
            if (string.IsNullOrEmpty(mediaId))
            {
                throw new ArgumentException("Cannot be null", "mediaId");
            }

            string url = String.Format("{0}{1}{2}/likes", ApiUrl, Section, mediaId);

            var parameters = new Dictionary<string, string>
                {
                    {"access_token",oAuth.AccesToken}  
                };

            var result = await HttpUtilities.Post(url, parameters);
            try
            {
                var jsonResult = await JsonConvert.DeserializeObjectAsync<Envelope<object>>(result);
                if (jsonResult.Meta.Code.Equals("200"))
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
           

        }

        public static async Task<bool> UnLike(OAuthResponse oAuth, string mediaId)
        {
            if (string.IsNullOrEmpty(mediaId))
            {
                throw new ArgumentException("Cannot be null", "mediaId");
            }

            string url = String.Format("{0}{1}{2}/likes?access_token={3}", ApiUrl, Section, mediaId,oAuth.AccesToken);

            try
            {
                var result = await HttpUtilities.Del(url);
                var jsonResult = await JsonConvert.DeserializeObjectAsync<Envelope<object>>(result);
                if (jsonResult.Meta.Code.Equals("200"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
           


        }

    }
}
