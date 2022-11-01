using System;
using CoreGraphics;
using XamarinEnterpriseApp.Xamarin.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(ExtendedTabPageRenderer))]
namespace XamarinEnterpriseApp.Xamarin.iOS.Renderers
{
    public class ExtendedTabPageRenderer : TabbedRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            OverrideUserInterfaceStyle = UIUserInterfaceStyle.Light;
        }
    }
}
