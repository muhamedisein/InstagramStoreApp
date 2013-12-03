using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WinInstagram.InstagramAPI.Models;
using WinInstagram.InstagramAPI.OAuth;
using WinInstagram.Utilites.Web;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace WinInstagram.InstagramAPI.Endpoints
{
    /// <summary>
    ///  Media Endpoint for Instagram API to get Media objects from Instagram.
    /// </summary>
    internal class MediaEndpoint : Endpoint
    {
        protected static string Section { get; set; }

        static MediaEndpoint()
        {
            Section = "/media/";
        }

        public static async Task<Envelope<Media>> MediaInfo(OAuthResponse oAuth, string mediaId)
        {
            string url = String.Format("{0}{1}{2}?access_token={3}", ApiUrl, Section, mediaId, oAuth.AccesToken);

            string result = await HttpUtilities.Get(url);
            try
            {
                var jsonResult = JsonConvert.DeserializeObject<Envelope<Media>>(result);
                return jsonResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static async Task<Envelope<ObservableCollection<Media>>> Popular(string url = null, OAuthResponse oAuth = null, uint count = 16)
        {
            string mainUrl = String.Format("{0}{1}popular?access_token={2}&count={3}", ApiUrl, Section, oAuth.AccesToken, count);

            var result = await HttpUtilities.Get(mainUrl);
            try
            {
                var jsonResult = await JsonConvert.DeserializeObjectAsync<Envelope<ObservableCollection<Media>>>(result);
                return jsonResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}