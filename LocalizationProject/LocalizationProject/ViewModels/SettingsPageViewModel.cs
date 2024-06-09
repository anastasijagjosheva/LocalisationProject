using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LocalizationProject.Localization;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LocalizationProject.ViewModels
{
    public class SettingsPageViewModel : BindableBase
    {
        private string _selectedTemperatureUnit;
        public string SelectedTemperatureUnit
        {
            get => _selectedTemperatureUnit;
            set
            {
                _selectedTemperatureUnit = value;
                RaisePropertyChanged(nameof(SelectedTemperatureUnit));
                Preferences.Set("TemperatureUnit", value);
                MessagingCenter.Send(this, "PreferenceChanged", "TemperatureUnit");
            }
        }
        
        private string _selectedWindSpeedUnit;
        public string SelectedWindSpeedUnit
        {
            get => _selectedWindSpeedUnit;
            set
            {
                _selectedWindSpeedUnit = value;
                RaisePropertyChanged(nameof(SelectedWindSpeedUnit));
                Preferences.Set("WindUnit", value);
                MessagingCenter.Send(this, "PreferenceChanged", "WindUnit");
            }
        }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                RaisePropertyChanged(nameof(SelectedLanguage));
                Preferences.Set("Language", value);
                var newCulture = new CultureInfo(_selectedLanguage);
                LocalizationResourceManager.Instance.SetCulture(newCulture);
                MessagingCenter.Send(this, "PreferenceChanged", "Language");
            }
        }

        public ICommand BackCommand { get; set; }
        
        private readonly INavigationService _navigation;
        
        public SettingsPageViewModel(INavigationService navigation)
        {
            _navigation = navigation;
            BackCommand = new Command(BackCommandHandler);
            SelectedTemperatureUnit = Preferences.Get("TemperatureUnit", "Celsius");
            SelectedWindSpeedUnit = Preferences.Get("WindUnit", "Kilometers per hour (km/h)");
            try
            {
                SelectedLanguage = Preferences.Get("Language", "en-us");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        
        private async void BackCommandHandler()
        {
            await _navigation.GoBackAsync();
        }
    }
}