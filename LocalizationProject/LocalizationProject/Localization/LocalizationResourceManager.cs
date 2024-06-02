using System.ComponentModel;
using System.Globalization;

namespace LocalizationProject.Localization
{
    public  class LocalizationResourceManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static LocalizationResourceManager _instance;
        public static LocalizationResourceManager Instance =>
            _instance ?? (_instance = new LocalizationResourceManager());
        
        public string this[string key] => AppResourcesHelper.GetString(key);

        public void SetCulture(CultureInfo culture)
        {
            CultureInfo.CurrentUICulture = culture;
            OnPropertyChanged(null); // Notify all properties have changed
        }

        
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}