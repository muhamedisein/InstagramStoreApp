using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using WinInstagram.InstagramAPI.OAuth;
using WinInstagram.ViewModel;
using WinInstagram.InstagramAPI.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using WinInstagram.Common;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace WinInstagram.Views
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class Popular
    {
        private readonly MediaGroupIncrementalLoad _mediaGroupIncrementalLoad = new MediaGroupIncrementalLoad("Popular",
                                                                                                     OAuthResponse
                                                                                                         .OAuthData
                                                                                                         .UserInfo.Id,
                                                                                                     16);

        public Popular()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            DefaultViewModel["MediaGroup"] = _mediaGroupIncrementalLoad;
            SizeChanged += Popular_SizeChanged;
            addHandlers();
            MediaInfoContent.FlipViewMediaSource = _mediaGroupIncrementalLoad.Items;
        }

        #region Helper Methods

        private void addHandlers()
        {
            _mediaGroupIncrementalLoad.Items.CollectonChanging += Items_CollectonChanging;
            _mediaGroupIncrementalLoad.Items.CollectionChanged += Items_CollectionChanged;
        }


        private void ResizeComponents()
        {
            HidePopUp(null, null);
            PopupContainer.Height = Frame.ActualHeight;
            double width;
            width = Window.Current.Bounds.Width < 800 ? 32 : 28;
            PopupContainer.Width = (double)new BarWidthConverter().Convert(null, null, width, "");
            RightBar.HorizontalOffset = Frame.ActualWidth - PopupContainer.Width;
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

        #endregion


        #region EventHandlers

        private async void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            LoadingBar.Visibility = Visibility.Collapsed;
            await Task.Delay(250);
            RefreshButton.Visibility = Visibility.Visible;
        }

        private void Items_CollectonChanging()
        {
            LoadingBar.Visibility = Visibility.Visible;
            RefreshButton.Visibility = Visibility.Collapsed;
        }


        private void Popular_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeComponents();
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

        private async void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            await _mediaGroupIncrementalLoad.RefreshData(16);
            MediaInfoContent.FlipViewMediaSource = _mediaGroupIncrementalLoad.Items;
            addHandlers();

        }

        #endregion

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
