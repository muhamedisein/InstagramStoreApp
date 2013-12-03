using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WinInstagram.Utilites.Web
{
    internal class HttpUtilities
    {
        public static async Task<string> Get(string url)
        {
            var client = new HttpClient();
            try
            {

                HttpResponseMessage responseMessage = await client.GetAsync(url);
                var result = await responseMessage.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public static async Task<String> Post(string url, IDictionary<string, string> parameters)
        {
            var client = new HttpClient();
            var data = parameters.ToList();
            var content = new FormUrlEncodedContent(data);
            HttpResponseMessage response = await client.PostAsync(url, content);
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }


        public static async Task<string> Del(string url)
        {
            var client = new HttpClient();
            try
            {
                HttpResponseMessage responseMessage = await client.DeleteAsync(url);
                var result = await responseMessage.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}