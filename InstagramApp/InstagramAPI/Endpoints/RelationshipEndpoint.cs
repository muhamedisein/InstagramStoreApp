using System.Collections.ObjectModel;
using WinInstagram.InstagramAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinInstagram.InstagramAPI.OAuth;
using WinInstagram.Utilites.Web;
using Newtonsoft.Json;

namespace WinInstagram.InstagramAPI.Endpoints
{
    /// <summary>
    ///  Relationship Endpoint for instagram API to change relationship status between users ( follow,unfollow,block etc)
    /// </summary>
    class RelationshipEndpoint : Endpoint
    {
        protected static string Section { get; set; }

        static RelationshipEndpoint()
        {
            Section = "/users/";
        }

        public static async Task<Relationship> Relationship(OAuthResponse oAuth, string userId)
        {
            string url = String.Format("{0}{1}{2}/relationship?access_token={3}", ApiUrl, Section, userId,
                                       oAuth.AccesToken);

            string data = await HttpUtilities.Get(url);

            var jsonResult = await JsonConvert.DeserializeObjectAsync<Envelope<Relationship>>(data);
            if (jsonResult.Meta.Code.Equals("200"))
            {
                return jsonResult.Data;
            }
            throw new Exception(jsonResult.Meta.Message);
        }

        public static async Task<ObservableCollection<UserInfo>> Follows(OAuthResponse oAuth, string userId)
        {

            string url = string.Format("{0}{1}{2}/follows?access_token={3}", ApiUrl, Section, userId, oAuth.AccesToken);

            var result = await HttpUtilities.Get(url);
            var jsonResult = await JsonConvert.DeserializeObjectAsync<Envelope<ObservableCollection<UserInfo>>>(result);

            if (jsonResult.Meta.Code.Equals("200"))
            {
                return jsonResult.Data;
            }
            throw new Exception(jsonResult.Meta.Message);

        }

        public static async Task<ObservableCollection<UserInfo>> FollowedBy(OAuthResponse oAuth, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Cannot be empty or null", "userId");
            }

            string url = String.Format("{0}{1}{2}/followed-by?access_token={3}", ApiUrl, Section, userId, oAuth.AccesToken);

            var result = await HttpUtilities.Get(url);

            var jsonResult = await JsonConvert.DeserializeObjectAsync<Envelope<ObservableCollection<UserInfo>>>(result);

            if (jsonResult.Meta.Code.Equals("200"))
            {
                return jsonResult.Data;
            }
            throw new Exception(jsonResult.Meta.Message);

        }

        public static async Task<bool> ChangeStatus(OAuthResponse oAuth, string userId, string action)
        {
            string url = String.Format("{0}{1}{2}/relationship", ApiUrl, Section, userId);
            var parameters = new Dictionary<string, string>()
                {
                    {"access_token", oAuth.AccesToken},
                    {"action", action}
                };

            var result = await HttpUtilities.Post(url, parameters);
            var jsonResult = await JsonConvert.DeserializeObjectAsync<Envelope<Object>>(result);
            if (jsonResult.Meta.Code.Equals("200"))
            {
                return true;
            }
                return false;
        


        }
    }
}
