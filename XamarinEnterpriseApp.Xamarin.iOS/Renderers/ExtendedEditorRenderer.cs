using System;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedEditor), typeof(ExtendedEditorRenderer))]
namespace XamarinEnterpriseApp.Xamarin.iOS.Renderers
{
    public class ExtendedEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control != null)
                {
                    var view = (ExtendedEditor)Element;

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