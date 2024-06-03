using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace LocalizationProject.Localization
{
    public static class AppResourcesHelper
    {
        private static readonly ResourceManager ResourceManager =
            new ResourceManager("LocalizationProject.LocalizationResources.AppResources", typeof(AppResourcesHelper).Assembly);
        
        public static string GetString(string key)
        {
            return ResourceManager.GetString(key, CultureInfo.CurrentUICulture);
        }
        
        
        
        public static Dictionary<string, string> GetAllStrings(CultureInfo culture)
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
    }
}