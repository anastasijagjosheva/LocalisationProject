using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using LocalizationProject.Models;
using LocalizationProject.Services.Location;
using LocalizationProject.Services.Weather;
using LocalizationProject.Views;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LocalizationProject.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware, IDestructible, IInitialize
    {
        private readonly ILocationService _locationService;
        private readonly IWeatherService _weatherService;
        
        private double _currentLat;
        private double _currentLon;
        
        public string CurrentCity { get; set; }
        public Location Location { get; set; }

        private WeatherDetails _weatherDetails;

        public WeatherDetails WeatherDetails
        {
            get => _weatherDetails;
            set => SetProperty(ref _weatherDetails, value);
        }

        private ObservableCollection<Weather> _dailyWeatherForecast;

        public ObservableCollection<Weather> DailyWeatherForecast
        {
            get => _dailyWeatherForecast;
            set => SetProperty(ref _dailyWeatherForecast, value);
        }

        private readonly INavigationService _navigation;
        public ICommand NavigateToSettingsCommand { get; }


        public MainPageViewModel(INavigationService navigation, IWeatherService weatherService, ILocationService locationService)
        {
            _navigation = navigation; 
            _weatherService = weatherService;
            _locationService = locationService;
            NavigateToSettingsCommand = new Command(NavigateToSettingsPage);
            
            /*Weathers = new ObservableCollection<Weather>()
            {
                new Weather()
                {
                    Temp = "22", Date = "Sunday 16", Icon = "weather.png"
                },
                new Weather()
                {
                    Temp = "21", Date = "Monday 17", Icon = "weather.png"
                },
                new Weather
                {
                    Temp = "20", Date = "Tuesday 18", Icon = "weather.png"
                },
                new Weather
                {
                    Temp = "12", Date = "Wednesday 19", Icon = "weather.png"
                },
                new Weather
                {
                    Temp = "17", Date = "Thursday 20", Icon = "weather.png"
                },
                new Weather
                {
                    Temp = "20", Date = "Friday 21", Icon = "weather.png"
                }
            };*/
        }

        private async Task<Location> GetCurrentLocationCoordinates()
        {
            try
            {
                var location = await _locationService.GetCurrentLocationCoordinates();
                return location;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }

        private async Task GetCurrentWeather()
        {
            try
            {
                var result = await _weatherService.GetCurrentWeatherAndDailyForecastByLatLon(_currentLat, _currentLon);
                WeatherDetails = result.WeatherDetails;
                DailyWeatherForecast = result.DailyWeatherForecast;
                
                Console.WriteLine("lalala");
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public  void OnNavigatedTo(INavigationParameters parameters)
        {
         
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        
        public async void  Initialize(INavigationParameters parameters)
        {
            Location = await Geolocation.GetLocationAsync();
            _currentLat = Location.Latitude;
            _currentLon = Location.Latitude;
            await GetCurrentWeather();
            Console.WriteLine("Stigna do location" + Location);
           // Location = GetCurrentLocationCoordinates().Result;
        }
        
        private async void NavigateToSettingsPage()
        {
            await _navigation.NavigateAsync("SettingsPage");
            //await _navigation.NavigateTo<SettingsPage>();
        }
    }
}