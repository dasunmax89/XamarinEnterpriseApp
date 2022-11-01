using System;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(ExtendedFrame), typeof(ExtendedFrameRenderer))]
namespace XamarinEnterpriseApp.Xamarin.iOS.Renderers
{
    public class ExtendedFrameRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            var view = (Frame)Element;

            if (view != null)
            {
                Layer.CornerRadius = 0;
                Layer.BorderColor = view.BorderColor.ToCGColor();
                Layer.BorderWidth = (float)1.0;
            }
        }
    }
}
