using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using LocalizationProject.Localization;
using LocalizationProject.Models;
using LocalizationProject.Services.Location;
using LocalizationProject.Services.Weather;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

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
            set
            {
                _weatherDetails = value;
                RaisePropertyChanged(nameof(WeatherDetails));
            }
        }

        private ObservableCollection<Weather> _dailyWeatherForecast;
        public ObservableCollection<Weather> DailyWeatherForecast
        {
            get => _dailyWeatherForecast;
            set
            {
                _dailyWeatherForecast = value;
                RaisePropertyChanged(nameof(DailyWeatherForecast));
            }
        }
        
        private string _temperatureUnit = "TemperatureUnit";
        public string TemperatureUnit
        {
            get => _temperatureUnit;
            set
            {
                _temperatureUnit = value;
                RaisePropertyChanged(nameof(TemperatureUnit));
            }
        }

        private string _windUnit = "WindUnit";
        public string WindUnit
        {
            get => _windUnit;
            set
            {
                _windUnit = value;
                RaisePropertyChanged(nameof(WindUnit));
            }
        }
        private string _language = "Language";
        public string Language
        {
            get => _language;
            set
            {
                _language = value;
                RaisePropertyChanged();
            }
        }

        private readonly INavigationService _navigation;
        public ICommand NavigateToSettingsCommand { get; }


        public MainPageViewModel(INavigationService navigation, IWeatherService weatherService,
            ILocationService locationService)
        {
            _navigation = navigation;
            _weatherService = weatherService;
            _locationService = locationService;
            NavigateToSettingsCommand = new Command(NavigateToSettingsPage);
            
            MessagingCenter.Subscribe<SettingsPageViewModel, string>(this, "PreferenceChanged", OnPreferenceChanged);
        }
        
        private void OnPreferenceChanged(SettingsPageViewModel sender, string key)
        {
            // Update values in MainPageViewModel when preferences change
            if (key == "TemperatureUnit")
            {
                TemperatureUnit = key;
                var newDailyWeatherForecast = new ObservableCollection<Weather>();
                _dailyWeatherForecast.ForEach(w =>
                {
                    newDailyWeatherForecast.Add(w);
                });
                
                DailyWeatherForecast = newDailyWeatherForecast;
                WeatherDetails = WeatherDetails;
            }
            else if (key == "WindUnit")
            {
                WindUnit = key;
                WeatherDetails = WeatherDetails;
                RaisePropertyChanged(WindUnit);
            }
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }
        
        public async void Initialize(INavigationParameters parameters)
        {
            Location = await Geolocation.GetLocationAsync();
            _currentLat = Location.Latitude;
            _currentLon = Location.Latitude;
            await GetCurrentWeather();
            // Location = GetCurrentLocationCoordinates().Result;
        }
        private async void NavigateToSettingsPage()
        {
            await _navigation.NavigateAsync("SettingsPage");
        }
    }
}