using System;
using Foundation;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedHtmlLabel), typeof(ExtendedHtmlLabelRenderer))]
namespace XamarinEnterpriseApp.Xamarin.iOS.Renderers
{
    public class ExtendedHtmlLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is ExtendedHtmlLabel element)
            {
                if (!string.IsNullOrEmpty(element.Text))
                {
                    var nsAttr = new NSAttributedStringDocumentAttributes
                    {
                        DocumentType = NSDocumentType.HTML
                    };

                    string appended = $"<p style='font-family:BasierCircle-Regular'>{element.Text}</p>";

                    var html = NSData.FromString(appended, NSStringEncoding.Unicode);
                    var nsError = new NSError();

                    Control.AttributedText = new NSAttributedString(html, nsAttr, ref nsError);
                }
            }
        }
    }
}
