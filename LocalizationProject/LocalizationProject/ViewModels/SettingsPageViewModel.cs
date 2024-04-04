using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
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
                Preferences.Set("temperatureUnit", value);
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
                Preferences.Set("windUnit", value);
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
                Preferences.Set("language", value);
            }
        }

        public ICommand BackCommand { get; set; }
        public ICommand TemperatureUnitSelectedCommand { get; private set; }
        public ICommand WindSpeedUnitSelectedCommand { get; private set; }
        public ICommand LanguageSelectedCommand { get; private set; }
        
        private readonly INavigationService _navigation;
        
        public SettingsPageViewModel(INavigationService navigation)
        {
            _navigation = navigation;
            BackCommand = new Command(BackCommandHandler);
            SelectedTemperatureUnit = Preferences.Get("temperatureUnit", "Celsius");
            SelectedWindSpeedUnit = Preferences.Get("windUnit", "Kilometers per hour (km/h)");
            SelectedLanguage = Preferences.Get("language", "English)");
            
        }
        
        private async void BackCommandHandler()
        {
            await _navigation.GoBackAsync();
        }
        
        /*
        private void OnTemperaturePickerSelectedItemChanged(object sender)
        {
            Picker picker = (Picker)sender;
            if (picker.SelectedIndex != -1)
            {
                SelectedTemperatureUnit = picker.SelectedItem.ToString();
                Preferences.Set("temperatureUnit", SelectedTemperatureUnit);
            }
        }
        
        private void OnWindSpeedPickerSelectedItemChanged(object sender)
        {
            Picker picker = (Picker)sender;
            if (picker.SelectedIndex != -1)
            {
                SelectedWindSpeedUnit = picker.SelectedItem.ToString();
                Preferences.Set("windUnit", SelectedWindSpeedUnit);

            }
        }
        
        private void OnLanguagePickerSelectedItemChanged(object sender)
        {
            Picker picker = (Picker)sender;
            if (picker.SelectedIndex != -1)
            {
                SelectedLanguage = picker.SelectedItem.ToString();
                Preferences.Set("language", SelectedLanguage);

            }
        }
        */
        
    }
}