using System;
using System.Drawing;
using CoreGraphics;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRenderer))]
namespace XamarinEnterpriseApp.Xamarin.iOS.Renderers
{
    public class RoundedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control != null)
                {
                    var view = (RoundedEntry)Element;

                    Control.LeftView = new UIView(new CGRect(0f, 0f, 9f, 20f));
                    Control.LeftViewMode = UITextFieldViewMode.Always;

                    Control.BorderStyle = UITextBorderStyle.None;

                    Control.Layer.CornerRadius = Convert.ToSingle(view.CornerRadius);
                    Control.Layer.BorderColor = view.BorderColor.ToCGColor();
                    Control.Layer.BorderWidth = view.BorderWidth;
                    Control.ClipsToBounds = true;

                    if (view.NextTextField != null)
                    {
                        Control.ReturnKeyType = UIReturnKeyType.Next;
                    }
                    else
                    {
                        Control.ReturnKeyType = UIReturnKeyType.Done;
                    }

                    RenderUtil.AddDoneAndNextButton(Control, view);
                }
            }
        }
    }
}