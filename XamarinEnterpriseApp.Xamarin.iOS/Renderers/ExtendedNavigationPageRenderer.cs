using System;
using CoreGraphics;
using XamarinEnterpriseApp.Xamarin.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(ExtendedNavigationPageRenderer))]
namespace XamarinEnterpriseApp.Xamarin.iOS.Renderers
{
    public class ExtendedNavigationPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            OverrideUserInterfaceStyle = UIUserInterfaceStyle.Light;
        }
    }
}
