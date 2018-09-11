#if UNITY_ANDROID
using UnityEngine;
#elif UNITY_IOS
using System.Runtime.InteropServices;
#elif UNITY_STANDALONE_WIN
using System.Runtime.InteropServices;
using System.Globalization;
#endif

public sealed class SystemLanguage
{
    public SystemLanguage()
    { }

    public static string GetLocaleCode(string defaultLocaleCode = "en")
    {
        string localeCode = null;
#if UNITY_ANDROID
            try
            {
                using (var locale = new AndroidJavaClass("java.util.Locale"))
                {
                    using (var defaultLocale = locale.CallStatic<AndroidJavaObject>("getDefault"))
                    {
                        localeCode = defaultLocale.Call<string>("getLanguage");
                    }
                }
            }
            catch (System.Exception)
            {
                localeCode = defaultLocaleCode;
            }
#elif UNITY_IOS
            localeCode = _getSystemLanguage();
            if (!string.IsNullOrEmpty(localeCode) && localeCode.Length > 2)
            {
                localeCode = localeCode.Substring(0, 2);
            }
#elif UNITY_STANDALONE_WIN        
        localeCode = GetSystemCulture().TwoLetterISOLanguageName;
#else
        localeCode = defaultLocaleCode;
#endif
        return localeCode;
    }

#if UNITY_IOS
        [DllImport("__Internal")]
        private static extern string _getSystemLanguage();
#endif

#if UNITY_STANDALONE_WIN
    [DllImport("KERNEL32.DLL")]
    private static extern int GetSystemDefaultLCID();

    private static CultureInfo GetSystemCulture()
    {
        return new CultureInfo(GetSystemDefaultLCID());
    }
#endif
}