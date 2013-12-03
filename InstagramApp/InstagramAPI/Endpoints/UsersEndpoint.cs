using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using WinInstagram.InstagramAPI.Models;
using WinInstagram.InstagramAPI.OAuth;
using WinInstagram.Utilites.Web;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace WinInstagram.InstagramAPI.Endpoints
{
    /// <summary>
    /// Users Endpoint for instagram API  for getting information about User.
    /// </summary>
    internal class UsersEndpoint : Endpoint
    {
        protected static string Section { get; set; }

        static UsersEndpoint()
        {
            Section = "/users/";

        }

        public static async Task<User> User(OAuthResponse oAuth, string userId)
        {
            string url = String.Format("{0}{1}{2}?access_token={3}", ApiUrl, Section, userId, oAuth.AccesToken);
            String data = await HttpUtilities.Get(url);
            var x = await JsonConvert.DeserializeObjectAsync<Envelope<User>>(data);
            if (x.Meta.Code.Equals("200"))
            {
                return x.Data;
            }
            throw new Exception(x.Meta.Message);
        }

        public static async Task<Envelope<ObservableCollection<Media>>> UserFeed(string url, OAuthResponse oAuth = null, uint count = 18)
        {
            string mainUrl;

            if (url == null && oAuth != null)
            {
                Debug.Assert(oAuth != null, "oAuth != null");
                mainUrl = String.Format("{0}{1}/self/feed/?access_token={2}&count={3}", ApiUrl, Section, oAuth.AccesToken, count);
            }
            else
                mainUrl = url;


            var rgx = new Regex(@"count=\d+");
            mainUrl = rgx.Replace(mainUrl, "count=" + count);
            var envelopeResult = await GetFromUrl<Media>(mainUrl);
            return envelopeResult;
        }

        public static async Task<Envelope<ObservableCollection<Media>>> MediaRecent(string url, OAuthResponse oAuth = null, string userId = null, uint count = 8)
        {
            string mainUrl;
            if (url == null && oAuth != null)
            {

                mainUrl = String.Format("{0}{1}{2}/media/recent/?access_token={3}&count={4}", ApiUrl, Section, userId,
                                        oAuth.AccesToken, count);
            }
            else
                mainUrl = url;

            var rgx = new Regex(@"count=\d+");
            mainUrl = rgx.Replace(mainUrl, "count=" + count);
            var envelopeResult = await GetFromUrl<Media>(mainUrl);
            return envelopeResult;
        }

        public static async Task<Envelope<ObservableCollection<Media>>> SelfLikedMedia(string url, OAuthResponse oAuth = null, uint count = 8)
        {
            string mainUrl;
            if (url == null && oAuth != null)
            {

                mainUrl = String.Format("{0}{1}self/media/liked/?access_token={2}&count={3}", ApiUrl, Section, oAuth.AccesToken, count);
            }
            else
                mainUrl = url;

            var rgx = new Regex(@"count=\d+");
            mainUrl = rgx.Replace(mainUrl, "count=" + count);
            var envelopeResult = await GetFromUrl<Media>(mainUrl);
            return envelopeResult;
        }

        public static async Task<UserInfo> UserInfoFromName(OAuthResponse oAuth, string userName, uint count = 1)
        {
            string url = string.Format("{0}{1}search?q={2}&count={3}&access_token={4}", ApiUrl, Section, userName, count, oAuth.AccesToken);


            var jsonObj = await GetFromUrl<UserInfo>(url);
            if (jsonObj.Meta.Code.Equals("200"))
            {

                return jsonObj.Data[0];
            }
            throw new Exception(jsonObj.Meta.Message);
        }


        public static async Task<ObservableCollection<UserInfo>> Search(OAuthResponse oAUth, string query)
        {
            string mainUrl = mainUrl = String.Format("{0}{1}search?q={2}&access_token={3}", ApiUrl, Section, query, oAUth.AccesToken);

            var evenlope = await GetFromUrl<UserInfo>(mainUrl);
            if (evenlope.Meta.Code.Equals("200"))
            {
                return evenlope.Data;
            }
            throw new Exception(evenlope.Meta.Message);


        }

        public static async Task<Envelope<ObservableCollection<T>>> GetFromUrl<T>(string url)
        {

            string data = await HttpUtilities.Get(url);
            var envelopeReslt = await JsonConvert.DeserializeObjectAsync<Envelope<ObservableCollection<T>>>(data);
            return envelopeReslt;
        }
    }
}
