using System;
using LocalizationProject.Localization;

namespace LocalizationProject.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToLocalizedString(this DateTime dateTime, string format)
        {
          return dateTime.ToString(format, LocalizationResourceManager.GetLanguageCultureInfo());
        }
    }
}