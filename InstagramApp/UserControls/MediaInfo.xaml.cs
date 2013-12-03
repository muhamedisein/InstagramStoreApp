using System;
using System.Collections.ObjectModel;
using WinInstagram.InstagramAPI.Endpoints;
using WinInstagram.InstagramAPI.Models;
using WinInstagram.InstagramAPI.OAuth;
using WinInstagram.ViewModel.Helpers;
using WinInstagram.Views;
using System.Linq;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;


namespace WinInstagram.UserControls
{
    /// <summary>
    /// UserControl for showing information about media ( user info, media info (likes, comments etc))
    /// </summary>
    public sealed partial class MediaInfo
    {
        public MediaInfo()
        {
            InitializeComponent();
        }

        static MediaInfo()
        {
            ParentFramePropety = DependencyProperty.Register("ParentFrame",
                                                             typeof(Frame),
                                                             typeof(MediaInfo),
                                                             null);
            FlipViewMediaSourceProperty = DependencyProperty.Register("FlipViewMediaSource",
                                                                      typeof(IncrementalLoadCollection<Media>),
                                                                      typeof(MediaInfo), null);
            ViewingProfileProperty = DependencyProperty.Register("ViewingProfile", typeof(bool), typeof(MediaInfo),
                                                                 new PropertyMetadata(false, OnViewingProfileChanged));
        }




        #region DependecyProperties

        private static readonly DependencyProperty FlipViewMediaSourceProperty;
        private static readonly DependencyProperty ParentFramePropety;
        private static readonly DependencyProperty ViewingProfileProperty;

        #endregion

        #region Properties

        private Envelope<ObservableCollection<Comment>> _commentsEnvelope;

        private Media _viewingMedia;

        public bool ViewingProfile
        {
            get { return (bool)GetValue(ViewingProfileProperty); }

            set { SetValue(ViewingProfileProperty, value); }
        }

        public IncrementalLoadCollection<Media> FlipViewMediaSource
        {
            get { return (IncrementalLoadCollection<Media>)GetValue(FlipViewMediaSourceProperty); }
            set { SetValue(FlipViewMediaSourceProperty, value); }
        }

        public Frame ParentFrame
        {
            private get { return (Frame)GetValue(ParentFramePropety); }
            set { SetValue(ParentFramePropety, value); }
        }

        #endregion

        #region EventHandlers

        private void UserNameTapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is TextBlock)
            {
                NavigateToUserProfile((sender as TextBlock).Tag.ToString());
            }
        }

        private async void FlipViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ImageFlipView.SelectedItem != null)
            {
                var media = e.AddedItems[0] as Media;
                if (media != null && _viewingMedia != null && media.User.Id == _viewingMedia.User.Id)
                {
                    _viewingMedia = media;
                    await LoadComments(media);
                }
                else
                {
                    _viewingMedia = media;
                    await RelationshipStatus(media);
                    await LoadComments(media);

                }

                if (_viewingMedia != null && _viewingMedia.UserHasLiked)
                {
                    VisualStateManager.GoToState(this, "MediaIsLiked", false);

                }

            }

        }

        private static void OnViewingProfileChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var info = d as MediaInfo;
            if (info != null)
            {
                if ((bool)e.NewValue)
                {
                    info.UserInfo.Visibility = Visibility.Collapsed;
                    info.UserInfoRect.Visibility = Visibility.Collapsed;
                }
                else
                {
                    info.UserInfo.Visibility = Visibility.Visible;
                    info.UserInfoRect.Visibility = Visibility.Visible;
                }
            }
        }

        private async void ImageDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

            if (_viewingMedia.UserHasLiked)
            {
                var unLiked = await LikesEndpoint.UnLike(OAuthResponse.OAuthData, _viewingMedia.Id);
                if (unLiked)
                {
                    VisualStateManager.GoToState(this, "Default", false);
                    _viewingMedia.UserHasLiked = false;
                    _viewingMedia.Likes.Count--;
                }

            }
            else
            {
                var liked = await LikesEndpoint.Like(OAuthResponse.OAuthData, _viewingMedia.Id);
                if (liked)
                {
                    VisualStateManager.GoToState(this, "MediaIsLiked", false);
                    _viewingMedia.UserHasLiked = true;
                    _viewingMedia.Likes.Count++;
                }

            }
        }

        private async void RelationshipDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var textBlock = sender as TextBlock;
            if (textBlock != null && !(await RelationshipAction(_viewingMedia.User.Id, textBlock.Tag.ToString())))
            {
                await new MessageDialog("Actio not completed").ShowAsync();
                RelationshipText.Visibility = Visibility.Collapsed;
                LoadingStatus.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        #region Methods

        public async void LoadSelectedMediaInfo(string selectedMediaId, Media m = null)
        {
            try
            {
                if (FlipViewMediaSource != null)
                {
                    if (ImageFlipView.ItemsSource == null)
                    {
                        ImageFlipView.ItemsSource = FlipViewMediaSource;
                    }
                    ImageFlipView.Visibility = Visibility.Visible;

                    _viewingMedia = FlipViewMediaSource.FirstOrDefault(media => media.Id == selectedMediaId);
                    ImageFlipView.SelectedIndex = FlipViewMediaSource.IndexOf(_viewingMedia);
                    await RelationshipStatus(_viewingMedia);
                    await LoadComments(_viewingMedia);
                }
            }
            catch (Exception)
            {
            }


        }

        private void NavigateToUserProfile(string userId)
        {
            if (!string.IsNullOrEmpty("userId")) ParentFrame.Navigate(typeof(ProfileView), userId);

        }

        private async Task LoadComments(Media media)
        {
            try
            {
                CommentsList.Visibility = Visibility.Collapsed;



                if (media.Comments.Count > media.Comments.CommentsList.Count)
                {
                    CommentsLoadRing.Visibility = Visibility.Visible;
                    _commentsEnvelope = await CommentsEndpoint.Comments(OAuthResponse.OAuthData, media.Id);
                    media.Comments.CommentsList = _commentsEnvelope.Data;
                    media.Comments.Count = media.Comments.CommentsList.Count;
                    CommentsLoadRing.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception)
            {
                //TO DO
                // log error
            }
            finally
            {

                CommentsList.Visibility = Visibility.Visible;
            }
        }

        

        private async void SendComment(object sender, RoutedEventArgs e)
        {
            var media = ImageFlipView.SelectedItem as Media;
            if (media != null) await CommentsEndpoint.PostComment(OAuthResponse.OAuthData, media.Id, CommentText.Text);
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

        private async Task RelationshipStatus(Media media)
        {
            RelationshipText.Visibility = Visibility.Collapsed;
            if (media.User.Id.Equals(OAuthResponse.OAuthData.UserInfo.Id))
            {

                return;
            }

            LoadingStatus.Visibility = Visibility.Visible;

            try
            {
                Relationship rel = await RelationshipEndpoint.Relationship(OAuthResponse.OAuthData, media.User.Id);
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

        #endregion


    }
}