using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using Xamarin.Forms;

namespace LocalizationProject.Localization
{
    public  class LocalizationResourceManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private static readonly ResourceManager ResourceManager =
            new ResourceManager("LocalizationProject.LocalizationResources.AppResources", typeof(LocalizationResourceManager).Assembly);

        private CultureInfo _cultureInfo = new CultureInfo("mk-mk");
        
        private static LocalizationResourceManager _instance;
        public static LocalizationResourceManager Instance =>
            _instance ?? (_instance = new LocalizationResourceManager());
        
        public StringResource this[string key] => this.GetString(key);

        private readonly List<StringResource> _resources = new List<StringResource>();
        public StringResource GetString(string resourceName)
        {
            string stringRes = ResourceManager.GetString(resourceName, CultureInfo.CurrentUICulture);
            var stringResource = new StringResource(resourceName, stringRes);
            _resources.Add(stringResource);
            return stringResource;
        }
        public void SetCulture(CultureInfo culture)
        {
            CultureInfo.CurrentUICulture = culture;
            _cultureInfo = culture;
            
            foreach (StringResource stringResource in _resources.ToList()) {
                stringResource.Value = ResourceManager.GetString(stringResource.Key, _cultureInfo);
            }
        }
    }
}