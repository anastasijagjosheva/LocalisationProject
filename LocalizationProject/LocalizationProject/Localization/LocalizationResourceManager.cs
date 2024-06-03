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

        private CultureInfo cultureInfo = new CultureInfo("mk-mk");
        
        private static LocalizationResourceManager _instance;
        public static LocalizationResourceManager Instance =>
            _instance ?? (_instance = new LocalizationResourceManager());
        
        //public string this[string key] => AppResourcesHelper.GetString(key);
        
        public StringResource this[string key] => this.GetString(key);

        private readonly List<StringResource> resources = new List<StringResource>();
        public StringResource GetString(string resourceName)
        {
            string stringRes = ResourceManager.GetString(resourceName, CultureInfo.CurrentUICulture);
            var stringResource = new StringResource(resourceName, stringRes);
            resources.Add(stringResource);
            return stringResource;
        }

        public void SetCulture(CultureInfo culture)
        {
            CultureInfo.CurrentUICulture = culture;
            cultureInfo = culture;
            
            PrintResources( new CultureInfo("de-de"));
            
            foreach (StringResource stringResource in resources.ToList()) {
                stringResource.Value = ResourceManager.GetString(stringResource.Key, cultureInfo);
            }
            
            
            
            //MessagingCenter.Send(this, "PreferenceChanged", "Language");
            
            //OnPropertyChanged(Item[]); // Notify all properties have changed
        }
        
        private  void PrintResources(CultureInfo culture)
        {
            Console.WriteLine($"Resources for culture: {culture.Name}");
            
            var allStrings = GetAllStrings(culture);

            foreach (var kvp in allStrings)
            {
                Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
            }
            Console.WriteLine();
        }
        
        public Dictionary<string, string> GetAllStrings(CultureInfo culture)
        {
            var resourceSet = ResourceManager.GetResourceSet(culture, true, true);
            var resources = new Dictionary<string, string>();

            foreach (var entry in resourceSet)
            {
                var dictionaryEntry = (System.Collections.DictionaryEntry)entry;
                resources.Add(dictionaryEntry.Key.ToString(), dictionaryEntry.Value.ToString());
            }

            return resources;
        }

        
        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}