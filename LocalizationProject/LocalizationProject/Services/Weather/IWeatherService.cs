using System.Threading.Tasks;
using LocalizationProject.Models;

namespace LocalizationProject.Services.Weather
{
    public interface IWeatherService
    {
        Task<CurrentWeather> GetCurrentWeatherAndDailyForecastByLatLon(double lat, double lon);
    }
}