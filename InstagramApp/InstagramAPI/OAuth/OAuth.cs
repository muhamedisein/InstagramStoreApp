using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinInstagram.InstagramAPI.Models;
using WinInstagram.Utilites.Web;
using Newtonsoft.Json;

namespace WinInstagram.InstagramAPI.OAuth
{
    /// <summary>
    ///  OAuth helper
    /// </summary>
    internal class OAuth
    {
        public enum Scope
        {
            basic,
            comments,
            relationships,
            likes
        }

        private ClientInfo _client;


        public OAuth()
        {
        }


        public OAuth(string url, ClientInfo client)
        {
            Url = url;
            Client = client;
        }

        public String Url { get; set; }

        public ClientInfo Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public string GetUrl(string responseType, List<Scope> scopes = null)
        {
            var url =
                new StringBuilder(string.Format("{0}/authorize/?client_id={1}&redirect_uri={2}&response_type={3}", Url,
                                                Client.CliendId, Client.RedirectUrl, responseType));
            if (scopes != null && scopes.Count > 0)
            {
                url.Append("&scope=" + scopes.First().ToString());

                foreach (Scope scope in scopes.Skip(1))
                {
                    url.Append("+" + scope.ToString());
                }
            }
            return url.ToString();
        }


        public async Task<OAuthResponse> GetAccesToken(string code)
        {
            var parameters = new Dictionary<string, string>
                {
                    {"client_id", _client.CliendId},
                    {"client_secret", _client.SecretId},
                    {"grant_type", "authorization_code"},
                    {"redirect_uri", _client.RedirectUrl},
                    {"code", code}
                };

            string accesUrl = Url + "/access_token";
            var result = await HttpUtilities.Post(accesUrl, parameters);
            var tokeResult = JsonConvert.DeserializeObject<OAuthResponse>(result);

            return tokeResult;
        }
    }
}