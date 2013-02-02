using System;
using System.Windows;
using System.Windows.Data;

namespace Notepad_Mobile
{
    public class BoolToVisibilityConverter: IValueConverter
    {

        public object Convert(object value, Type targetType, 
            object parameter, System.Globalization.CultureInfo culture)
        {
            bool bVal;
            if (bool.TryParse(value.ToString(), out bVal))
                return bVal ? Visibility.Visible : Visibility.Collapsed;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, 
            object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility vis = Visibility.Collapsed;

            try
            {
                vis = (Visibility)Enum.Parse(typeof(Visibility),
                    (string)value, true);
                return vis;
            }
            catch
            {
                return vis;
            }
        }
    }
}
