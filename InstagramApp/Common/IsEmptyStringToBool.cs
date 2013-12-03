using System;
using Windows.UI.Xaml.Data;

namespace WinInstagram.Common
{
    /// <summary>
    /// Converter that translates String content to bool
    /// 
    /// </summary>
    /// 
    class IsEmptyStringToBool:IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns>
        ///     true if the string is empty.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return  value.ToString().Length > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
