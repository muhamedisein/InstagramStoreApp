namespace WinInstagram.InstagramAPI.Models
{
    internal class ClientInfo
    {
        public ClientInfo()
        {
        }

        public ClientInfo(string clientId, string secredId, string webUrl, string redirectUrl)
        {
            CliendId = clientId;
            SecretId = secredId;
            WebUrl = webUrl;
            RedirectUrl = redirectUrl;
        }

        public string CliendId { get; set; }
        public string SecretId { get; set; }
        public string WebUrl { get; set; }
        public string RedirectUrl { get; set; }
    }
}