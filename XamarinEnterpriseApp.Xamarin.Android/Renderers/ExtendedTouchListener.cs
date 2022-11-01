using System;
using Android.Views;

namespace XamarinEnterpriseApp.Xamarin.Droid.Renderers
{
    public class ExtendedTouchListener : Java.Lang.Object, View.IOnTouchListener
    {
        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            v.Parent?.RequestDisallowInterceptTouchEvent(true);
            if ((e.Action & MotionEventActions.Up) != 0 && (e.ActionMasked & MotionEventActions.Up) != 0)
            {
                v.Parent?.RequestDisallowInterceptTouchEvent(false);
            }
            return false;
        }
    }
}
