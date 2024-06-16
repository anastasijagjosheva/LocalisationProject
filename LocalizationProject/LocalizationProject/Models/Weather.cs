using System;
using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace LocalizationProject.Models
{
    public class Weather : BindableBase
    {
        public DateTime Date { get; set; }
        
        public string Icon { get; set; } = "weather.png";
        
        private double _temp;
        public double Temp
        {
            get => _temp;
            set
            {
                _temp = value;
                RaisePropertyChanged(nameof(Temp));
            }
        }
        public string TempUnitIcon { get; set; } = "°C";
    }
    
    public class WeatherDetails
    {
        public DateTime DateTime{ get; set; }
        public double Temp { get; set; }
        
        public string TempUnitIcon { get; set; } = "°C";
        public int Humidity { get; set; }
        public double Wind { get; set; }
        public double Pressure { get; set; }
        public int Cloudiness { get; set; }
        public string Icon { get; set; } = "weather.png";

    }
        
    
    public class CurrentWeather
    {
        public WeatherDetails WeatherDetails { get; set; }
        public ObservableCollection<Weather> DailyWeatherForecast { get; set; }
    }
    
}