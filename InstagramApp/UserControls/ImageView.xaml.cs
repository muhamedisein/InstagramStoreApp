using System.Threading.Tasks;
using Windows.UI.Xaml;
using WinInstagram.InstagramAPI.Models;
using Windows.UI.Xaml.Input;
using WinInstagram.Utilites;
namespace WinInstagram.UserControls
{
    /// <summary>
    /// UserControl for viewing images which shows custom loading content while the iamge is loading.
    /// </summary>
    public sealed partial class ImageView
    {
        private static readonly DependencyProperty MediaProperty = DependencyProperty.Register("Media", typeof(Media), typeof(ImageView),
                                                                             new PropertyMetadata(null, MediaChanged));

        private static readonly DependencyProperty LoadingContentPropery = DependencyProperty.Register("LoadingContent", typeof(object),
                                                                                       typeof(ImageView), null);

        private static readonly DependencyProperty FailedContentProperty = DependencyProperty.Register("FailedContent", typeof(object),
                                                                                      typeof(ImageView), null);
        

        public Media Media
        {
            get { return (Media)GetValue(MediaProperty); }
            set { SetValue(MediaProperty, value); }
        }

        public object LoadingContent
        {
            get { return GetValue(LoadingContentPropery); }
            set { SetValue(LoadingContentPropery, value); }
        }

        public object FailedContent
        {
            get { return GetValue(FailedContentProperty); }
            set { SetValue(FailedContentProperty, value); }
        }


        public ImageView()
        {
            InitializeComponent();
           // MainGrid.Width = Constants.ImaegeWidth;
           // MainGrid.Height = Constants.ImaegeWidth;
            MainGrid.Width = 280;
            MainGrid.Height = 280; 
        }



        private static void MediaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var imgView = (ImageView)d;
            VisualStateManager.GoToState(imgView, "LoadingData", true);
        }


        private void Image_ImageOpened(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Displaying", true);
        }

    }
}