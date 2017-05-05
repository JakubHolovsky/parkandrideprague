using System.Globalization;

namespace ParkAndRidePrague.Localization
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale(CultureInfo ci);
    }
}
