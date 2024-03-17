using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LocalizationProject.Core;
using LocalizationProject.Models;
using Newtonsoft.Json.Linq;

namespace LocalizationProject.Services.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClientFactory _httpClientFactory;

        public WeatherService(HttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CurrentWeather> GetCurrentWeatherAndDailyForecastByLatLon(double lat, double lon)
        {
            var baseUrl = "https://api.open-meteo.com/v1/forecast?";
            //var response = await _httpClientFactory.GetHttpClient().GetAsync($"{baseUrl}latitude={lat}&longitude={lon}&current=temperature_2m,relative_humidity_2m,cloud_cover,pressure_msl,wind_speed_10m");
            var response = await _httpClientFactory.GetHttpClient().GetAsync(
                $"{baseUrl}latitude={lat}&longitude={lon}&current=temperature_2m,relative_humidity_2m,cloud_cover,pressure_msl,wind_speed_10m&daily=temperature_2m_max");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(result);
                
                WeatherDetails weatherDetails = new WeatherDetails()
                {
                    DateTime = DateTime.Now,
                    Temp = Convert.ToInt32(json["current"]?["temperature_2m"]),
                    Humidity = Convert.ToInt32(json["current"]?["relative_humidity_2m"]),
                    Wind = Convert.ToDouble(json["current"]?["wind_speed_10m"]),
                    Pressure = Convert.ToInt32(json["current"]?["pressure_msl"]),
                    Cloudiness = Convert.ToInt32(json["current"]?["cloud_cover"])
                };

                var dailyForecast = new ObservableCollection<Models.Weather>();
                
                
                for (var i = 0; i < 7; i++)
                {
                    var lal = json["daily"]?["time"]?[i];

                    dailyForecast.Add(new Models.Weather()
                    {
                        Date = Convert.ToDateTime(json["daily"]?["time"][i]),
                        Temp = Convert.ToInt32(json["daily"]?["temperature_2m_max"][i]),
                    });
                }

                CurrentWeather currentWeather = new CurrentWeather()
                {
                    WeatherDetails = weatherDetails,
                    DailyWeatherForecast = dailyForecast
                };
                
                Console.WriteLine(currentWeather);

                return currentWeather;
            }

            return null;
        }


        //https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current=temperature_2m,relative_humidity_2m,cloud_cover,pressure_msl,wind_speed_10m
    }
}