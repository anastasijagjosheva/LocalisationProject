using System.Globalization;
using System.Resources;

namespace LocalizationProject.Localization
{
    public static class AppResourcesHelper
    {
        private static readonly ResourceManager ResourceManager =
            new ResourceManager("LocalizationProject.LocalizationResources.AppResources", typeof(AppResourcesHelper).Assembly);
        
        //LocalizationProject.LocalizationResources.AppResources
        //LocalizationResources.AppResources
        public static string GetString(string key)
        {
            return ResourceManager.GetString(key, CultureInfo.CurrentUICulture);
        }
    }
}