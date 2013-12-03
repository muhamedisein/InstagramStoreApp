using System;
using System.Collections.Generic;
using WinInstagram.Common;
using WinInstagram.InstagramAPI.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using WinInstagram.ViewModel;
using WinInstagram.InstagramAPI.OAuth;
using WinInstagram.InstagramAPI.Endpoints;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace WinInstagram.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Search
    {
        private readonly UserCollectionVm _users = new UserCollectionVm();
        public Search()
        {
            InitializeComponent();
            DefaultViewModel["SearchResult"] = _users;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {

        }


        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ApplicationView.TryUnsnap();
        }

        private void NavigateToProfile(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null)
            {
                var userInfo = e.ClickedItem as UserInfo;
                if (userInfo != null)
                    Frame.Navigate(typeof(ProfileView), userInfo.Id);
            }
        }

        private async void QuerySearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(QuerySearch.Text) && QuerySearch.Text.Length > 1)
            {
                if ('@'.Equals(QuerySearch.Text[0]))
                {

                    LoadingBar.Visibility = Visibility.Visible;
                    await _users.ReadData(async delegate
                    {
                        var data = await UsersEndpoint.Search(OAuthResponse.OAuthData, QuerySearch.Text.Substring(1));
                        return data;
                    });
                    LoadingBar.Visibility = Visibility.Collapsed;
                }
                else if ('#'.Equals(QuerySearch.Text[0]))
                {
                    //TO Do 
                }
                else
                {
                    await new MessageDialog("Search for @username ").ShowAsync();
                }
            }
            else
            {
                _users.Users.Clear();
            }


        }



    }

}
