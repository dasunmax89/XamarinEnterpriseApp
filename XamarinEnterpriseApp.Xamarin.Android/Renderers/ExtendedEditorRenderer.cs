﻿using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Text.Method;
using Android.Views;
using Android.Views.InputMethods;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Views.View;

[assembly: ExportRenderer(typeof(ExtendedEditor), typeof(ExtendedEditorRenderer))]
namespace XamarinEnterpriseApp.Xamarin.Droid.Renderers
{
    public class ExtendedEditorRenderer : EditorRenderer
    {
        public ExtendedEditorRenderer(Context context)
            : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var view = (ExtendedEditor)Element;

                var gradientBackground = new GradientDrawable();
                gradientBackground.SetShape(ShapeType.Rectangle);
                gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                // Thickness of the stroke line
                gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

                var xx = RenderUtil.DpToPixels(this.Context,
                        Convert.ToSingle(view.CornerRadius));

                // Radius for the curves
                gradientBackground.SetCornerRadius(view.CornerRadius);

                Background = gradientBackground;

                Control.Background = gradientBackground;

                Control.SetPadding(
                        (int)RenderUtil.DpToPixels(this.Context, Convert.ToSingle(5)),
                        (int)RenderUtil.DpToPixels(this.Context, Convert.ToSingle(2)),
                        (int)RenderUtil.DpToPixels(this.Context, Convert.ToSingle(10)),
                        (int)RenderUtil.DpToPixels(this.Context, Convert.ToSingle(0)));

                //twice to tap editor to keyboard open
                //Control.SetCursorVisible(true);
                //Control.SetTextIsSelectable(true);
                RenderUtil.AddDoneAndNextButton(Control, view, view.IsMultiline);

                RenderUtil.SetScrollable(Control);
            }
        }
    }
}
