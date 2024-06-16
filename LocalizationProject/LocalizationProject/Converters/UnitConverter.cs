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
            var param = (string)parameter;

            var temperatureUnit = Preferences.Get("TemperatureUnit", "Celsius");
            var windUnit = Preferences.Get("WindUnit", "Kilometers per hour (km/h)");
            var pressureUnit = Preferences.Get("PressureUnit", "hPa");
            var language = Preferences.Get("Language", "English");

            switch (param)
            {
                case "TemperatureUnit":
                    return temperatureUnit.Equals("°C")
                        ? $"{val}"
                        : $"{val * 9 / 5 + 32}";
                case "WindUnit":
                    return windUnit.Equals("km/h")
                        ? $"{Math.Round(val, 1)} km/h"
                        : $"{Math.Round((val * 1000 / 3600), 1)} mps";
                case "PressureUnit":
                    return pressureUnit.Equals("hPa")
                        ? $"{Math.Round(val, 1)} hPa"
                        : $"{Math.Round((val / 1000), 1)} bar";
                case "TempUnitIcon":
                    return temperatureUnit.Equals("°C") ? "°C" : "°F";
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