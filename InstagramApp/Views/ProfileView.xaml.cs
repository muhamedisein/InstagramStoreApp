using System;
using System.Collections.Generic;
using WinInstagram.Common;
using WinInstagram.InstagramAPI.Endpoints;
using WinInstagram.InstagramAPI.Models;
using WinInstagram.InstagramAPI.OAuth;
using WinInstagram.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Input;
using Windows.UI;
using Windows.UI.Xaml.Media;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;



// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace WinInstagram.Views
{
    /// <summary>
    ///     A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ProfileView
    {
        private MediaGroupIncrementalLoad _mediaGroup;
        private readonly UserCollectionVm _usersCollection = new UserCollectionVm();
        private readonly UserViewModel _userSrc = new UserViewModel();
        private ResourceLoader loader = new Windows.ApplicationModel.Resources.ResourceLoader();

        public ProfileView()
        {
            InitializeComponent();
            DefaultViewModel["UserModel"] = _userSrc;
            DefaultViewModel["UsersList"] = _usersCollection;
            SizeChanged += ProfileView_SizeChanged;


        }

        private void ProfileView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeComponents();
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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UserGridView.Visibility = Visibility.Collapsed;
            ApplicationView.TryUnsnap();
            if (e.Parameter != null)
            {
                var useId = e.Parameter.ToString();
                if (useId != null) _userSrc.User.Id = useId;
            }
            string error = null;
            try
            {
                LoadingBar.Visibility =Visibility.Visible;
                await _userSrc.RaedData();
                UserGridView.Visibility = Visibility.Visible;
                await RelationshipStatus();
               _mediaGroup = new MediaGroupIncrementalLoad("Recent Media", _userSrc.User.Id,
                                                                (uint)_userSrc.User.Counts.Media);
                PageTitle.Text = _userSrc.User.UserName;
                DefaultViewModel["MediaGroup"] = _mediaGroup;
                LoadingBar.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            if (!string.IsNullOrEmpty(error))
            {
                await new MessageDialog(error).ShowAsync();
                Frame.GoBack();
            }
        }

        private void ResizeComponents()
        {
            PopupContainer.Height = Frame.ActualHeight;
            double width;
            width = Window.Current.Bounds.Width < 800 ? 32 : 28;
            PopupContainer.Width = (double)new BarWidthConverter().Convert(null, null, width, "");
            RightBar.HorizontalOffset = Frame.ActualWidth - PopupContainer.Width;
        }

        private void HidePopUp(object sender, RoutedEventArgs e)
        {
            RightBar.IsOpen = false;
        }

        private void ShowMediaInfo(object sender, ItemClickEventArgs e)
        {

            RelationshipInfo.Visibility = Visibility.Collapsed;
            MediaInfoContent.Visibility = Visibility.Visible;
            var x = e.ClickedItem as Media;
            if (x != null)
            {
                MediaInfoContent.LoadSelectedMediaInfo(x.Id);
                showPopUp();
            }
        }

        public void showPopUp()
        {
            ApplicationView.TryUnsnap();
            RightBar.IsOpen = true;
        }

        private async void ShowFollowers(object sender, RoutedEventArgs e)
        {

            try
            {
                InitBarForUsersList();
                showPopUp();

                BarTitle.Text = loader.GetString("Following/Text") + " :";
                await _usersCollection.ReadData(async delegate
                    {
                        var data = await RelationshipEndpoint.Follows(OAuthResponse.OAuthData, _userSrc.User.Id);
                        return data;
                    });
                ShowUsersList();
            }
            catch
            {
                
            }
         
        }

        private async void ShowFollowedBy(object sender, RoutedEventArgs e)
        {
            try
            {

                InitBarForUsersList();
                showPopUp();
                BarTitle.Text = loader.GetString("Followers/Text") + " :";
                await _usersCollection.ReadData(async delegate
                    {
                        var data = await RelationshipEndpoint.FollowedBy(OAuthResponse.OAuthData, _userSrc.User.Id);
                        return data;
                    });
                ShowUsersList();
            }
            catch (Exception)
            {

            }
        }

        private void InitBarForUsersList()
        {
            MediaInfoContent.Visibility = Visibility.Collapsed;
            RelationshipInfo.Visibility = Visibility.Visible;
            RightBarLoading.Visibility = Visibility.Visible;
            UsersListView.Visibility = Visibility.Collapsed;
        }

        private void ShowUsersList()
        {
            RightBarLoading.Visibility = Visibility.Collapsed;
            UsersListView.Visibility = Visibility.Visible;
        }

        private void NavigateToUser(object sender, ItemClickEventArgs e)
        {
            var user = e.ClickedItem as UserInfo;
            if (user != null)
            {
                Frame.Navigate(typeof(ProfileView), user.Id);
            }
        }



        private async void RelationshipDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var textBlock = sender as TextBlock;
            if (textBlock != null && !(await RelationshipAction(_userSrc.User.Id, textBlock.Tag.ToString())))
            {
                await new MessageDialog("Actio not completed").ShowAsync();
                RelationshipText.Visibility = Visibility.Collapsed;
                LoadingStatus.Visibility = Visibility.Collapsed;
            }
        }

        private void SetFollows()
        {
            RelationshipText.Text = "Unfollow";
            RelationshipText.Tag = "unfollow";
            RelationshipText.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void SetUnfollow()
        {
            RelationshipText.Text = "Follow";
            RelationshipText.Tag = "follow";
            RelationshipText.Foreground = Application.Current.Resources["LightGreen"] as SolidColorBrush;
        }

        private async Task RelationshipStatus()
        {
            RelationshipText.Visibility = Visibility.Collapsed;
            if (_userSrc.User.Id.Equals(OAuthResponse.OAuthData.UserInfo.Id))
            {
                return;
            }

            LoadingStatus.Visibility = Visibility.Visible;

            try
            {
                Relationship rel = await RelationshipEndpoint.Relationship(OAuthResponse.OAuthData, _userSrc.User.Id);
                switch (rel.OutgoingStatus)
                {
                    case "follows":
                        SetFollows();
                        break;
                    case "none":
                        SetUnfollow();
                        break;

                }
                LoadingStatus.Visibility = Visibility.Collapsed;
                RelationshipText.Visibility = Visibility.Visible;
            }
            catch
            {
                RelationshipText.Visibility = Visibility.Collapsed;
                LoadingStatus.Visibility = Visibility.Collapsed;
            }



        }



        private async Task<bool> RelationshipAction(string userId, string action)
        {
            try
            {
                RelationshipText.Visibility = Visibility.Collapsed;
                LoadingStatus.Visibility = Visibility.Visible;
                if (await RelationshipEndpoint.ChangeStatus(OAuthResponse.OAuthData, userId, action))
                {
                    switch (action)
                    {
                        case "follow":
                            SetFollows();
                            break;
                        case "unfollow":
                            SetUnfollow();
                            break;

                    }
                    LoadingStatus.Visibility = Visibility.Collapsed;
                    RelationshipText.Visibility = Visibility.Visible;
                    return true;
                }


                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}