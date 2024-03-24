using System;
using LocalizationProject.Models;
using LocalizationProject.Services.Location;
using LocalizationProject.Services.Weather;
using LocalizationProject.ViewModels;
using LocalizationProject.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Essentials;
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