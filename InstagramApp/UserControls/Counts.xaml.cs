using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace WinInstagram.UserControls
{
    /// <summary>
    /// User control to show information about user profile counts ( media, follows, followed by )
    /// </summary>
    public sealed partial class Counts : UserControl
    {
        public static readonly DependencyProperty CountProperty = DependencyProperty.Register("Count",
                                                                                              typeof (
                                                                                                  InstagramAPI.Models.
                                                                                                  Counts),
                                                                                              typeof (Counts), null);

        public event FollowersClickDelegate FollowersClick;
        public delegate void FollowersClickDelegate(object sender, RoutedEventArgs e);


        void OnFollowersClick(object sender, RoutedEventArgs e)
        {
            if (FollowersClick != null)
                FollowersClick(this, e);
        }

        public event RoutedEventDel FollowedClick;
        public delegate void RoutedEventDel(object sender, RoutedEventArgs e);

       

        void OnFollowedClick(object sender, RoutedEventArgs e)
        {
            if (FollowedClick != null)
                FollowedClick(this, e);
        }
       

        public Counts()
        {
            InitializeComponent();
        }

        public InstagramAPI.Models.Counts Count
        {
            get { return ((InstagramAPI.Models.Counts) GetValue(CountProperty)); }
            set { SetValue(CountProperty, value); }
        }

      
    }
}