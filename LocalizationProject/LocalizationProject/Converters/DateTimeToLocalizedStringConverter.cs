using System;
using System.Globalization;
using LocalizationProject.Extensions;
using Xamarin.Forms;

namespace LocalizationProject.Converters
{
    public class DateTimeToLocalizedStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                string format = parameter as string ?? "f"; 
                return dateTime.ToLocalizedString(format); 
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}