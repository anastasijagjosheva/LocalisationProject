using Prism.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalizationProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ViewModelLocator.SetAutowireViewModel(this,true);  
        }
    }
}