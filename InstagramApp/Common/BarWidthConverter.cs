using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace WinInstagram.Common
{
    /// <summary>
    ///     Converter for bar width
    /// </summary>

    class BarWidthConverter : IValueConverter
    {
        /// <summary>
        ///     Converter to get percentage value from window current width
        /// </summary>
        /// <param name="parameter">
        ///     Percentage you want to get from the window width
        /// </param>
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            double x;
            if (Double.TryParse(parameter.ToString(), out x))
            {
                return Window.Current.Bounds.Width * (x / 100);
            }
            else
            {
                return 0;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
