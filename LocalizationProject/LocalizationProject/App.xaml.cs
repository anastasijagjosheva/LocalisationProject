using System.Globalization;
using LocalizationProject.Localization;
using LocalizationProject.LocalizationResources;
using LocalizationProject.Services.Location;
using LocalizationProject.Services.Weather;
using LocalizationProject.ViewModels;
using LocalizationProject.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace LocalizationProject
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            
            var culture = new CultureInfo("mk-mk");
            LocalizationResourceManager.Instance.SetCulture(culture);
            AppResources.Culture = culture;
            
            await NavigationService.NavigateAsync("MainPage");
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();

            containerRegistry.RegisterForNavigation<CakesCatalog, CakesCatalogViewModel>();
            
            containerRegistry.RegisterSingleton<ILocationService, LocationService>();
            containerRegistry.RegisterSingleton<IWeatherService, WeatherService>();
        }
        
    }
}