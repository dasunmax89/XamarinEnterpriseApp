using System;
using Android.App;
using Android.OS;

namespace XamarinEnterpriseApp.Xamarin.Droid.Dependency
{
    public static class Toolkit
    {
        public static void Init(Activity activity, Bundle bundle)
        {
            Activity = activity;
        }

        public static Activity Activity { private set; get; }
    }
}
