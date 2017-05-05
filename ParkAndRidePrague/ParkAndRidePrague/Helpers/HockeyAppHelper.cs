namespace ParkAndRidePrague.Helpers
{
    public static class HockeyAppHelper
    {
#if __ANDROID__
        public static string AppId = "fbe2aba529ad47a998d4001030bae780";
#else
#if __IOS__
        public static string AppId = "";
#else
        public static string AppId = "a866256586744f5385267c7b0be943eb";
#endif
#endif
    }
}
