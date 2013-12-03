using System;
using System.Diagnostics;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace WinInstagram.Utilites.LocalData
{
 
    internal class LocalSettings
    {
        public static Object GetLocalSetting(string name)
        {
            IPropertySet values = ApplicationData.Current.LocalSettings.Values;
            if (values != null && values[name] != null)
            {
                return values[name];
            }
            return null;
        }

        public static bool SetLocalSettings<T>(String name, T value)
        {
            IPropertySet values = ApplicationData.Current.LocalSettings.Values;
            try
            {
                values[name] = value;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}