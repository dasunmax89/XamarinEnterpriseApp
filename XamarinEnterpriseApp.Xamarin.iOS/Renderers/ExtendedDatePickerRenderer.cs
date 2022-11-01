using System;
using CoreGraphics;
using XamarinEnterpriseApp.Xamarin.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePicker), typeof(ExtendedDatePickerRenderer))]
namespace XamarinEnterpriseApp.Xamarin.iOS.Renderers
{
    public class ExtendedDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var view = (DatePicker)Element;
                UIDatePicker dateTimePicker = (UIDatePicker)Control.InputView;

                dateTimePicker.Mode = UIDatePickerMode.DateAndTime;

                //Control.LeftView = new UIView(new CGRect(0f, 0f, 5f, 5f));
                Control.LeftViewMode = UITextFieldViewMode.Never;

                Control.Layer.CornerRadius = 0f;
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
                Control.ClipsToBounds = true;
            }
        }
    }
}
