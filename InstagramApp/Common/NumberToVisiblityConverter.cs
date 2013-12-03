using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;
namespace WinInstagram.Common
{
    /// <summary>
    /// Number to bool converter, 0 is false, everythng else is true
    /// </summary>
    class NumberToVisiblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            if(value == null) return Visibility.Collapsed;
            return (int) value == 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
