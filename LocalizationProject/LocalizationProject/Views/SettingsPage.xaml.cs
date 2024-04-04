using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalizationProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            ViewModelLocator.SetAutowireViewModel(this,true);  
        }
    }
}