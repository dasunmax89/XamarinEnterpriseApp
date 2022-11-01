using System;
using Android.Content;
using Android.Graphics.Drawables;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedFrame), typeof(ExtendedFrameRenderer))]
namespace XamarinEnterpriseApp.Xamarin.Droid.Renderers
{
    public class ExtendedFrameRenderer : FrameRenderer
    {
        public ExtendedFrameRenderer(Context context)
           : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var view = (ExtendedFrame)Element;

                // creating gradient drawable for the curved background
                var gradientBackground = new GradientDrawable();
                gradientBackground.SetShape(ShapeType.Rectangle);
                gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                // Thickness of the stroke line
                gradientBackground.SetStroke(2, Color.FromHex("d8d8d8").ToAndroid());

                // Radius for the curves
                gradientBackground.SetCornerRadius(
                    RenderUtil.DpToPixels(this.Context,
                        Convert.ToSingle(view.CornerRadius)));

                // set the background of the label
                Background = gradientBackground;
            }
        }
    }
}
