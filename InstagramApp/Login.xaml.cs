using System;
using System.Collections.Generic;
using WinInstagram.InstagramAPI.Models;
using WinInstagram.InstagramAPI.OAuth;
using WinInstagram.Utilites;
using WinInstagram.Utilites.LocalData;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WinInstagram
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login
    {
        private static OAuth _oauth;
        public Login()
        {
            InitializeComponent();
            InitAuth();
            InitWebView();
            string url = CreateUrlForCode();
           LoginWebView.Navigate(new Uri(url));
          
        }

        public string CreateUrlForCode()
        {
            var scope = new List<OAuth.Scope>
                {
                    OAuth.Scope.basic,
                    OAuth.Scope.comments,
                    OAuth.Scope.likes,
                    OAuth.Scope.relationships
                };
            return _oauth.GetUrl("code", scope);
        }

        public void InitAuth()
        {
            _oauth = new OAuth("https://instagram.com/oauth", new ClientInfo
                {
                    CliendId = "53bfa681955b4ee9b9b5e14587994838",
                    SecretId = "cc32746cd41c4d49a914fd39f54c6a1c",
                    WebUrl = "http://hipsterapp.azurewebsites.net/",
                    RedirectUrl = "http://hipsterapp.azurewebsites.net/index.html"
                });
        }

        public void InitWebView()
        {
            LoginWebView = new WebView();
            Grid.SetRow(LoginWebView,1);
            MainContent.Children.Add(LoginWebView);
            LoginWebView.ScriptNotify += webView_ScriptNotify;
            var allowedUris = new List<Uri> {new Uri(_oauth.Client.RedirectUrl)};
            LoginWebView.AllowedScriptNotifyUris = allowedUris;
        }

        private async void webView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            LoadingBar.Visibility = Visibility.Visible;
            LoginText.Visibility = Visibility.Collapsed;
            LoginWebView.Visibility = Visibility.Collapsed;
            string code = e.Value;
            OAuthResponse res = await _oauth.GetAccesToken(code);
            await LocalFIles.SetFileContent(Constants.OAuthSavedData, res);
            OAuthResponse.OAuthData = res;
            await LocalFIles.SetFileContent(Constants.OAuthSavedData, res);
            Frame.Navigate(typeof (MainPlaceHolder), true);
        }

        /// <summary>
        ///     Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">
        ///     Event data that describes how this page was reached.  The Parameter
        ///     property is typically used to configure the page.
        /// </param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           /* InitAuth();
            InitWebView();
            string url = CreateUrlForCode();
            LoginWebView.Navigate(new Uri(url));-
            LoginWebView.Navigate(new Uri(url));-*/
        }
    }
}