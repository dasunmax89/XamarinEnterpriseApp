using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedButton), typeof(ExtendedButtonRenderer))]
namespace XamarinEnterpriseApp.Xamarin.Droid.Renderers
{
    public class ExtendedButtonRenderer : ButtonRenderer
    {
        public ExtendedButtonRenderer(Context context)
           : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var view = (ExtendedButton)Element;

                var textAlignment = view.TextAlignment;

                if (textAlignment == "Start")
                {
                    Control.Gravity = GravityFlags.Start;
                }
                else if (textAlignment == "Center")
                {
                    Control.Gravity = GravityFlags.Center;
                }
                else if (textAlignment == "End")
                {
                    Control.Gravity = GravityFlags.End;
                }

                // creating gradient drawable for the curved background
                var gradientBackground = new GradientDrawable();
                gradientBackground.SetShape(ShapeType.Rectangle);
                gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                // Thickness of the stroke line
                gradientBackground.SetStroke((int)view.BorderWidth, view.BorderColor.ToAndroid());

                // Radius for the curves
                gradientBackground.SetCornerRadius(
                    RenderUtil.DpToPixels(this.Context,
                        Convert.ToSingle(view.CornerRadius)));

                // set the background of the label
                Control.SetBackground(gradientBackground);
            }
        }
    }
}
