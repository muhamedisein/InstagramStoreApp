using System;
using System.Collections.Generic;
using WinInstagram.Common;
using WinInstagram.InstagramAPI.OAuth;
using WinInstagram.Utilites.LocalData;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WinInstagram.Views;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace WinInstagram
{
    /// <summary>
    ///     A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPlaceHolder
    {
        public MainPlaceHolder()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Populates the page with content passed during navigation.  Any saved state is also
        ///     provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">
        ///     The parameter value passed to
        ///     <see cref="Frame.Navigate(Type, Object)" /> when this page was initially requested.
        /// </param>
        /// <param name="pageState">
        ///     A dictionary of state preserved by this page during an earlier
        ///     session.  This will be null the first time a page is visited.
        /// </param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        ///     Preserves state associated with this page in case the application is suspended or the
        ///     page is discarded from the navigation cache.  Values must conform to the serialization
        ///     requirements of <see cref="SuspensionManager.SessionState" />.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                var firstTime = e.Parameter.ToString();
                if (firstTime.Equals("true", StringComparison.OrdinalIgnoreCase))
                    Frame.SetNavigationState("1,0");

                ContentProvider.Navigate(typeof(UserFeed));
            }
        }

       
        private void NavigateToUserProfile(object sender, RoutedEventArgs e)
        {
            
           ContentProvider.SetNavigationState("1,0");
           ContentProvider.Navigate(typeof (ProfileView), OAuthResponse.OAuthData.UserInfo.Id);
        }

     
        private void NavigateToNewsFeed(object sender, RoutedEventArgs e)
        {
            ContentProvider.SetNavigationState("1,0");
            ContentProvider.Navigate(typeof(UserFeed));
            
        }

        private void NavigateToPopular(object sender, RoutedEventArgs e)
        {
            ContentProvider.SetNavigationState("1,0");
            ContentProvider.Navigate(typeof(Popular));
        }

        private void NavigateToSearch(object sender, RoutedEventArgs e)
        {
            ContentProvider.SetNavigationState("1,0");
            ContentProvider.Navigate(typeof(Search));
        }

        private async void LogOutUser(object sender, RoutedEventArgs e)
        {
            await LocalFIles.checkACFolder();
            Frame.Navigate(typeof (Login));
        }
    }
}