namespace WinInstagram.InstagramAPI.Endpoints
{
    /// <summary>
    /// Base class for Instagram Endpoint
    /// </summary>
    internal class Endpoint
    {

        /// <summary>
        ///  Instagram ApiUrl for sending requests
        /// </summary>
        protected static string ApiUrl
        {
            get { return "https://api.instagram.com/v1"; }
        }

    }
}