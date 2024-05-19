using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;
using Xamarin.Forms;

namespace LocalizationProject.Localization
{
    public class TranslateManager 
    {
        //private static ILocale _locale = DependencyService.Get<ILocale>();
        public static string ResourceId { get; private set; }
        public static Assembly ResourceAssembly { get; private set; }
        public static ResourceManager ResourceManager { get; private set; }
        private static CultureInfo _languageCulture { get; set; }
        
        public static void SetLanguage(string cultureName)
        {
            _languageCulture = string.IsNullOrEmpty(cultureName) ? CultureInfo.InvariantCulture : new CultureInfo(cultureName);
           // Thread.CultureInfo.CurrentUICulture = _languageCulture;
            Thread.CurrentThread.CurrentUICulture = _languageCulture;

            //MessagingCenter.Send(this, "PreferenceChanged", "Language");



            //_locale.SetThreadUiCulture(_languageCulture);
        }
    }
}