using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Xamarin.Forms.Internals;

namespace LocalizationProject.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware, IDestructible, IInitialize
    {
        private readonly ILocationService _locationService;
        private readonly IWeatherService _weatherService;

        private double _currentLat;
        private double _currentLon;

        private bool IsMainPageViewModelActive;

        public string CurrentCity { get; set; }
        public Location Location { get; set; }

        private WeatherDetails _weatherDetails;

        public WeatherDetails WeatherDetails
        {
            get => _weatherDetails;
            //set => SetProperty(ref _weatherDetails, value);
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
            //set => SetProperty(ref _dailyWeatherForecast, value);
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

        private readonly INavigationService _navigation;
        public ICommand NavigateToSettingsCommand { get; }


        public MainPageViewModel(INavigationService navigation, IWeatherService weatherService,
            ILocationService locationService)
        {
            _navigation = navigation;
            _weatherService = weatherService;
            _locationService = locationService;
            NavigateToSettingsCommand = new Command(NavigateToSettingsPage);

            /*// Subscribe to preference change messages
            MessagingCenter.Subscribe<SettingsPageViewModel, string>(this, "PreferenceChanged", (sender, key) =>
            {
                // Update values in MainPageViewModel when preferences change
                if (key == "TemperatureUnit" || key == "WindSpeedUnit")
                {
                   // OnPropertyChanged(key); // Notify subscribers of the property change
                    RaisePropertyChanged(nameof(key));
                }
            });*/

            // Example of initial values

            MessagingCenter.Subscribe<SettingsPageViewModel, string>(this, "PreferenceChanged", OnPreferenceChanged);

            IsMainPageViewModelActive = true;
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

                Console.WriteLine("lalala");
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
            // Subscribe to preference change messages
            /*MessagingCenter.Subscribe<SettingsPageViewModel, string>(this, "PreferenceChanged", (sender, key) =>
            {

               Debug.WriteLine($"Received parameter: {key}");

                // Update values in MainPageViewModel when preferences change
                if (key == "TemperatureUnit" || key == "WindSpeedUnit")
                {
                   // OnPropertyChanged(key); // Notify subscribers of the property change
                   TemperatureUnit = key;
                }
            });*/
            //MessagingCenter.Subscribe<SettingsPageViewModel, string>(this, "PreferenceChanged", OnPreferenceChanged);
        }

        public void Destroy()
        {
            IsMainPageViewModelActive = false;
            throw new NotImplementedException();
        }


        public async void Initialize(INavigationParameters parameters)
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