using System;
using System.Collections.ObjectModel;

namespace LocalizationProject.Models
{
    public class Weather
    {
        public DateTime Date { get; set; }
        public string Icon { get; set; } = "weather.png";
        public int Temp { get; set; }
    }


    public class WeatherDetails
    {
        public DateTime DateTime{ get; set; }
        public double Temp { get; set; }
        public int Humidity { get; set; }
        public double Wind { get; set; }
        public int Pressure { get; set; }
        public int Cloudiness { get; set; }
    }
        
    
    public class CurrentWeather
    {
        public WeatherDetails WeatherDetails { get; set; }
        public ObservableCollection<Weather> DailyWeatherForecast { get; set; }
    }
    
}