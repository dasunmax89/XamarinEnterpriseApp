using System;
using System.Globalization;
using System.Threading.Tasks;
using Foundation;
using XamarinEnterpriseApp.Xamarin.Core;
using XamarinEnterpriseApp.Xamarin.iOS.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(PreferencesManager))]
namespace XamarinEnterpriseApp.Xamarin.iOS.Dependency
{
    public class PreferencesManager : IPreferencesManager
    {
        static readonly object locker = new object();

        const string Settings = "Settings";

        public Task PlatformSet<T>(string key, T value)
        {
            lock (locker)
            {
                using (var userDefaults = GetUserDefaults(Settings))
                {
                    if (value == null)
                    {
                        if (userDefaults[key] != null)
                            userDefaults.RemoveObject(key);
                        return Task.CompletedTask;
                    }

                    switch (value)
                    {
                        case string s:
                            userDefaults.SetString(s, key);
                            break;
                        case int i:
                            userDefaults.SetInt(i, key);
                            break;
                        case bool b:
                            userDefaults.SetBool(b, key);
                            break;
                        case long l:
                            var valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
                            userDefaults.SetString(valueString, key);
                            break;
                        case double d:
                            userDefaults.SetDouble(d, key);
                            break;
                        case float f:
                            userDefaults.SetFloat(f, key);
                            break;
                    }
                }
            }

            return Task.CompletedTask;
        }

        public T PlatformGet<T>(string key, T defaultValue)
        {
            object value = null;

            lock (locker)
            {
                using (var userDefaults = GetUserDefaults(Settings))
                {
                    if (userDefaults[key] == null)
                        return defaultValue;

                    switch (defaultValue)
                    {
                        case int i:
                            value = (int)(nint)userDefaults.IntForKey(key);
                            break;
                        case bool b:
                            value = userDefaults.BoolForKey(key);
                            break;
                        case long l:
                            var savedLong = userDefaults.StringForKey(key);
                            value = Convert.ToInt64(savedLong, CultureInfo.InvariantCulture);
                            break;
                        case double d:
                            value = userDefaults.DoubleForKey(key);
                            break;
                        case float f:
                            value = userDefaults.FloatForKey(key);
                            break;
                        case string s:
                            // the case when the string is not null
                            value = userDefaults.StringForKey(key);
                            break;
                        default:
                            // the case when the string is null
                            if (typeof(T) == typeof(string))
                                value = userDefaults.StringForKey(key);
                            break;
                    }
                }
            }

            return (T)value;
        }

        private NSUserDefaults GetUserDefaults(string sharedName)
        {
            if (!string.IsNullOrWhiteSpace(sharedName))
                return new NSUserDefaults(sharedName, NSUserDefaultsType.SuiteName);
            else
                return NSUserDefaults.StandardUserDefaults;
        }

        public PreferencesManager()
        {

        }
    }
}
