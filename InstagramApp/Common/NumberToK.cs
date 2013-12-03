using System;

using Windows.UI.Xaml.Data;

namespace WinInstagram.Common
{
    /// <summary>
    /// Converter for numbers
    /// </summary>
    class NumberToK:IValueConverter
    {
        /// <summary>
        ///     
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns>
        ///     Short format of number with K or M extension
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double number = double.Parse(value.ToString());
            if (number < 1000) return number;

            if(number < 1000000)
                return  String.Format("{0:0.##}", number/1000) + "K";

            return String.Format("{0:0.###}", number/1000000) + "M";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
