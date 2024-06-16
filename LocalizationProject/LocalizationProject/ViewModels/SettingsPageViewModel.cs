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
        
        private string _selectedPressureUnit;
        public string SelectedPressureUnit
        {
            get => _selectedPressureUnit;
            set
            {
                _selectedPressureUnit = value;
                RaisePropertyChanged(nameof(SelectedPressureUnit));
                Preferences.Set("PressureUnit", value);
                MessagingCenter.Send(this, "PreferenceChanged", "PressureUnit");
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
        
        private Color _backgroundColor1 = Color.LightGray;
        public Color BackgroundColor1
        {
            get => _backgroundColor1;
            set => SetProperty(ref _backgroundColor1, value);
            
        }
        private Color _backgroundColor2 = Color.LightGray;
        public Color BackgroundColor2
        {
            get => _backgroundColor2;
            set => SetProperty(ref _backgroundColor2, value);
        }
        public ICommand BackCommand { get; set; }
        public Command OnEnButtonClickedCommand { get; set; }
        public Command OnMkButtonClickedCommand { get; set; }
        
        private readonly INavigationService _navigation;
        
        public SettingsPageViewModel(INavigationService navigation)
        {
            _navigation = navigation;
            BackCommand = new Command(BackCommandHandler);
            OnEnButtonClickedCommand = new Command(OnENButtonClicked);
            OnMkButtonClickedCommand = new Command(OnMKButtonClicked);
            SelectedTemperatureUnit = Preferences.Get("TemperatureUnit", "Celsius");
            SelectedWindSpeedUnit = Preferences.Get("WindUnit", "Kilometers per hour (km/h)");
            SelectedPressureUnit = Preferences.Get("PressureUnit", "hPa");
            
            var selectedLanguage = Preferences.Get("Language", "en-us");
            if (selectedLanguage == "mk-mk")
            {
                OnMKButtonClicked();
            }
            else
            {
                OnENButtonClicked();
            }
            
            try
            {
                //SelectedLanguage = Preferences.Get("Language", "en-us");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        public void OnENButtonClicked()
        {
            BackgroundColor1 = Color.White;
            BackgroundColor2 = Color.LightGray;
            
            Preferences.Set("Language", "en-us");
            var newCulture = new CultureInfo("en-us");
            LocalizationResourceManager.Instance.SetCulture(newCulture);
            MessagingCenter.Send(this, "PreferenceChanged", "Language");
        }

        public void OnMKButtonClicked()
        {
            BackgroundColor1 = Color.LightGray;
            BackgroundColor2 = Color.White;
            
            Preferences.Set("Language", "mk-mk");
            var newCulture = new CultureInfo("mk-mk");
            LocalizationResourceManager.Instance.SetCulture(newCulture);
            MessagingCenter.Send(this, "PreferenceChanged", "Language");
        }
        private async void BackCommandHandler()
        {
            await _navigation.GoBackAsync();
        }
    }
}