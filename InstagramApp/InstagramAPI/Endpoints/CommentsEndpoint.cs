using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WinInstagram.InstagramAPI.Models;
using WinInstagram.InstagramAPI.OAuth;
using WinInstagram.Utilites.Web;
using Newtonsoft.Json;

namespace WinInstagram.InstagramAPI.Endpoints
{
    /// <summary>
    /// Comments endpoint for InstagramAPi. ALlows to get, post comments for media.
    /// </summary>
    class CommentsEndpoint:Endpoint
    {
        protected static string Section { get; set; }

        static CommentsEndpoint()
        {
            Section = "/media/";
        }
        /// <summary>
        /// Posta a comment on media
        /// </summary>
        /// <param name="oAuth">
        ///       OAuthResponse object with information of authorizad user
        /// </param>
        /// <param name="mediaId">
        ///     ID of the media to post a commment on
        /// </param>
        /// <param name="text">
        ///     Content of the comment
        /// </param>
        /// <returns></returns>
       public static async Task<bool> PostComment(OAuthResponse oAuth, string mediaId, string text)
       {
           string url = String.Format("{0}{1}{2}/comments", ApiUrl, Section, mediaId);
           var parameters = new Dictionary<string, string>
               {
                   {"access_token", oAuth.AccesToken},
                   {   "text",  text  }  
               };

           var result = await HttpUtilities.Post(url, parameters);
           var jsonResult = await JsonConvert.DeserializeObjectAsync<Envelope<Object>>(result);

           if (jsonResult.Meta.Code.Equals("200"))
               return true;

           return false;
       }

        /// <summary>
        /// Get list of comments for media
        /// </summary>
        /// <param name="oAuth">
        ///    OAuthResponse object with information of authorizad user
        /// </param>
        /// <param name="mediaId">
        ///     ID of media
        /// </param>
        /// <returns></returns>
        public static async Task<Envelope<ObservableCollection<Comment>>> Comments(OAuthResponse oAuth, string mediaId)
        {
            #region Arguments validation
            if (oAuth == null)
            {
                throw new ArgumentNullException("oAuth", "User must be authenticated");
            }
            if (string.IsNullOrEmpty(mediaId))
            {
                throw new ArgumentException("mediaId can't be null or empty");
            }
            #endregion

            string url = String.Format("{0}{1}{2}/comments?access_token={3}", ApiUrl, Section, mediaId,OAuthResponse.OAuthData.AccesToken);
            string result = await HttpUtilities.Get(url);

            var jsonResult = await JsonConvert.DeserializeObjectAsync<Envelope<ObservableCollection<Comment>>>(result);
            
            if (jsonResult.Meta.Code.Equals("200"))
            {
                return jsonResult;
            }
            else
            {
                throw new Exception(jsonResult.Meta.Message);
            }
            
        } 
    }
}