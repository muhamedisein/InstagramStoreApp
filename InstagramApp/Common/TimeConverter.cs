using System;
using Windows.UI.Xaml.Data;

namespace WinInstagram.Common
{
    /// <summary>
    ///  Unix timestamp to windows timestamp
    /// </summary>
    class TimeConverter:IValueConverter
    {
        DateTime _createdTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns>
        ///     Time in windows timestamp.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            
            double unixTime = System.Convert.ToDouble(value);
            _createdTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            _createdTime = _createdTime.AddSeconds(unixTime).ToLocalTime();
            DateTime now = DateTime.Now;

           
            if (now.Subtract(_createdTime).TotalMinutes < 60)
            {
                return string.Format("{0:0}",now.Subtract(_createdTime).TotalMinutes ) + " minutes ago";
            }
            if (now.Subtract(_createdTime).TotalHours < 24)
            {
                return  string.Format("{0:0} hours ago",now.Subtract(_createdTime).TotalHours );
            }
            return _createdTime.ToString("MMM dd HH:MM");


        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
