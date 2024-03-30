using System;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace LocalizationProject.ViewModels
{
    public class SettingsPageViewModel
    {
        public string SelectedTemperatureUnit { get; set; }
        public string SelectedWindSpeedUnit { get; set; }
      
        public string SelectedLanguage{ get; set; }

        public ICommand BackCommand { get; set; }
        public ICommand TemperatureUnitSelectedCommand { get; private set; }
        public ICommand WindSpeedUnitSelectedCommand { get; private set; }
        public ICommand LanguageSelectedCommand { get; private set; }
        
        private readonly INavigationService _navigation;
        
        public SettingsPageViewModel(INavigationService navigation)
        {
            _navigation = navigation;
            BackCommand = new Command(BackCommandHandler);
            TemperatureUnitSelectedCommand = new Command(OnTemperaturePickerSelectedItemChanged);
            WindSpeedUnitSelectedCommand = new Command(OnWindSpeedPickerSelectedItemChanged);
            LanguageSelectedCommand = new Command(OnLanguagePickerSelectedItemChanged);
        }
        
        private async void BackCommandHandler()
        {
            await _navigation.GoBackAsync();
        }
        
        private void OnTemperaturePickerSelectedItemChanged(object sender)
        {
            Picker picker = (Picker)sender;
            if (picker.SelectedIndex != -1)
            {
                SelectedTemperatureUnit = picker.SelectedItem.ToString();
            }
        }
        
        private void OnWindSpeedPickerSelectedItemChanged(object sender)
        {
            Picker picker = (Picker)sender;
            if (picker.SelectedIndex != -1)
            {
                SelectedWindSpeedUnit = picker.SelectedItem.ToString();
            }
        }
        
        private void OnLanguagePickerSelectedItemChanged(object sender)
        {
            Picker picker = (Picker)sender;
            if (picker.SelectedIndex != -1)
            {
                SelectedLanguage = picker.SelectedItem.ToString();
            }
        }
    }
}