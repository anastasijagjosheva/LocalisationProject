using System;
using System.ComponentModel;
using System.Globalization;
using ImTools;
using LocalizationProject.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LocalizationProject.Converters
{
    public class UnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (double)value;
            Console.WriteLine(val);
            
            var param = (string)parameter;
            Console.WriteLine(param);
            
            var temperatureUnit = Preferences.Get("TemperatureUnit", "Celsius");
            Console.WriteLine(temperatureUnit);
            
            var windUnit= Preferences.Get("WindUnit", "Kilometers per hour (km/h)");
            Console.WriteLine(windUnit);

            var language= Preferences.Get("Language", "English");
            Console.WriteLine(language);

            switch(param)
            {
                case "TemperatureUnit": 
                    return temperatureUnit.Equals("Celsius") ? $"{val}": $"{val * 9 / 5 + 32}";
                case "WindUnit": 
                    return windUnit.Equals("Kilometers per hour (km/h)") ? $"{Math.Round(val, 1)} km/h" : $"{Math.Round((val * 1000 / 3600), 1)} mps";
                case "Language": 
                    return language.Equals("English") ? "C" : "F";
                default: 
                    return Math.Round(val, 1);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}