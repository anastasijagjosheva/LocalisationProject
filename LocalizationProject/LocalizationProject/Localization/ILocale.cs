using System.Globalization;

namespace LocalizationProject.Localization
{
    public interface ILocale
    {
        CultureInfo GetDeviceCultureInfo();

        void SetThreadCulture(CultureInfo cultureInfo);

        void SetThreadUiCulture(CultureInfo cultureInfo);
    }
}