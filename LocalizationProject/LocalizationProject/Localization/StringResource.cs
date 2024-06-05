using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LocalizationProject.Localization
{
    public class StringResource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public StringResource(string key, string value)
        {
            Key = key;
            Value = value;
        }

        private string value;

        public string Key { get; }

        public string Value {
            get => value;
            set {
                this.value = value;
                OnPropertyChanged();
            }
        }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}