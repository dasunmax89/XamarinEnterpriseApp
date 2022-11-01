using System;
using Android.Content;
using Android.Graphics.Drawables;
using Google.Android.Material.BottomNavigation;
using XamarinEnterpriseApp.Xamarin.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(ExtendedTabbedPageRenderer))]
namespace XamarinEnterpriseApp.Xamarin.Droid.Renderers
{
    public class ExtendedTabbedPageRenderer : TabbedPageRenderer
    {
        private BottomNavigationView _bottomNavigationView;

        public ExtendedTabbedPageRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                _bottomNavigationView = (GetChildAt(0) as Android.Widget.RelativeLayout).GetChildAt(1) as BottomNavigationView;
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            // Create a Gradient Stroke as the new top border. (Set alpha if needed.)
            GradientStrokeDrawable topBorderLine = new GradientStrokeDrawable { Alpha = 0x33 };
            // Change it to the color you want.
            topBorderLine.SetStroke(1, Color.FromHex("#DCDFE2").ToAndroid());

            LayerDrawable layerDrawable = new LayerDrawable(new Drawable[] { topBorderLine });

            layerDrawable.SetLayerInset(0, 0, 0, 0, _bottomNavigationView.Height - 2);

            _bottomNavigationView.SetBackground(layerDrawable);
        }

    }
}
