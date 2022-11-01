using System;
using Android.Content;
using Android.Text;
using Android.Widget;
using AndroidX.Core.Text;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedHtmlLabel), typeof(ExtendedHtmlLabelRenderer))]
namespace XamarinEnterpriseApp.Xamarin.Droid.Renderers
{
    public class ExtendedHtmlLabelRenderer : LabelRenderer
    {
        public ExtendedHtmlLabelRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is ExtendedHtmlLabel element)
            {
                if (!string.IsNullOrEmpty(element.Text))
                {
                    string appended = $"<p style='font-family:BasierCircle-Regular!important;'>{element.Text}</p>";

                    Control.SetText(HtmlCompat.FromHtml(appended, 0), TextView.BufferType.Spannable);
                }
            }
        }
    }
}
