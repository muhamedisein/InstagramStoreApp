using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using WinInstagram.InstagramAPI.OAuth;
using WinInstagram.ViewModel;
using WinInstagram.InstagramAPI.Models;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using WinInstagram.Common;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using WinInstagram.Utilites;

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace WinInstagram.Views
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class UserFeed
    {
        private MediaGroupIncrementalLoad _mediaGroupIncrementalLoad = new MediaGroupIncrementalLoad("Home", OAuthResponse.OAuthData.UserInfo.Id, 350);
        public UserFeed()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            addEventHandlers();
            Binding();
            SizeChanged += UserFeed_SizeChanged;


        }

        private void Binding()
        {
            addEventHandlers();
            DefaultViewModel["MediaGroup"] = _mediaGroupIncrementalLoad;
            MediaInfoContent.FlipViewMediaSource = _mediaGroupIncrementalLoad.Items;

        }

        private  void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            LoadingBar.Visibility = Visibility.Collapsed;
            RefreshButton.Visibility = Visibility.Visible;
        }

        private  void Items_CollectonChanging()
        {
            LoadingBar.Visibility = Visibility.Visible;
            RefreshButton.Visibility = Visibility.Collapsed;
        }

        private void UserFeed_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeComponents();
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

        private void addEventHandlers()
        {
            _mediaGroupIncrementalLoad.Items.CollectonChanging += Items_CollectonChanging;
            _mediaGroupIncrementalLoad.Items.CollectionChanged += Items_CollectionChanged;
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var x = e.ClickedItem as Media;
            if (x != null)
            {
                MediaInfoContent.LoadSelectedMediaInfo(x.Id);
                ShowPopUp();
            }

        }

        private void ResizeComponents()
        {
            double width;
            width = Window.Current.Bounds.Width < 800 ? 32 : 28;
            HidePopUp(null, null);
            PopupContainer.Height = Frame.ActualHeight;
            PopupContainer.Width = (double)new BarWidthConverter().Convert(null, null, width, "");
            RightBar.HorizontalOffset = Frame.ActualWidth - PopupContainer.Width;
            RightBar.Visibility = Visibility.Collapsed;
        }

        private void HidePopUp(object sender, RoutedEventArgs e)
        {
            RightBar.Visibility = Visibility.Collapsed;
            RightBar.IsOpen = false;
        }

        private void ShowPopUp()
        {

            RightBar.Visibility = Visibility.Visible;
            RightBar.IsOpen = true;
        }


        private async void RefreshData_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            MediaGridView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            MediaGridView.ScrollIntoView(_mediaGroupIncrementalLoad.Items[0]);
            MediaGridView.Visibility = Windows.UI.Xaml.Visibility.Visible;
            await Task.Delay(10);
            await _mediaGroupIncrementalLoad.RefreshData(12);
           
            // gluposti
            //_mediaGroupIncrementalLoad.Items.Clear();
            //_mediaGroupIncrementalLoad = new MediaGroupIncrementalLoad("Home", OAuthResponse.OAuthData.UserInfo.Id, 120);
            //await Task.Delay(250);
            //Binding();
            // await _mediaGroupIncrementalLoad.Items.LoadMoreItemsAsync(12);


        }

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var x = e.ClickedItem as Media;
            if (x != null)
            {
                ApplicationView.TryUnsnap();
                await Task.Delay(100);
                MediaInfoContent.LoadSelectedMediaInfo(x.Id);
                ShowPopUp();
            }
        }

    }
}
