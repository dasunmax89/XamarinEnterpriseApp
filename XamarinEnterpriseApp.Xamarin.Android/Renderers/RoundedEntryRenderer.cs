using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRenderer))]
namespace XamarinEnterpriseApp.Xamarin.Droid.Renderers
{
    public class RoundedEntryRenderer : EntryRenderer
    {
        public RoundedEntryRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var view = (RoundedEntry)Element;

                // creating gradient drawable for the curved background
                var gradientBackground = new GradientDrawable();
                gradientBackground.SetShape(ShapeType.Rectangle);
                gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                // Thickness of the stroke line
                gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

                // Radius for the curves
                gradientBackground.SetCornerRadius(
                    RenderUtil.DpToPixels(this.Context,
                        Convert.ToSingle(view.CornerRadius)));

                // set the background 
                Background = gradientBackground;

                Control.SetBackground(gradientBackground);

                int padding = 10;

                if (view.CustomPadding > 0)
                {
                    padding = view.CustomPadding;
                }

                // Set padding for the internal text from border
                Control.SetPadding(
                     (int)RenderUtil.DpToPixels(this.Context, Convert.ToSingle(5)),
                     (int)RenderUtil.DpToPixels(this.Context, Convert.ToSingle(padding)),
                     (int)RenderUtil.DpToPixels(this.Context, Convert.ToSingle(10)),
                     (int)RenderUtil.DpToPixels(this.Context, Convert.ToSingle(padding)));

                Control.Gravity = GravityFlags.CenterVertical;

                RenderUtil.AddDoneAndNextButton(Control, view);
            }
        }
    }
}
