using System;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Controls
{
    public class ExtendedEditor : Editor, INavigatableInput
    {
        public static readonly BindableProperty NextTextFieldProperty =
           BindableProperty.Create(nameof(NextTextField),
                                   typeof(InputView), typeof(ExtendedEditor), null);

        public InputView NextTextField
        {
            get => (InputView)GetValue(NextTextFieldProperty);
            set => SetValue(NextTextFieldProperty, value);
        }

        public static readonly BindableProperty IsMultilineProperty =
           BindableProperty.Create(nameof(IsMultiline),
                                   typeof(bool), typeof(ExtendedEditor), false);

        public bool IsMultiline
        {
            get => (bool)GetValue(IsMultilineProperty);
            set => SetValue(IsMultilineProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty =
         BindableProperty.Create(nameof(CornerRadius),
             typeof(int), typeof(ExtendedEditor), 0);

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor),
                typeof(Color), typeof(ExtendedEditor), Color.Gray);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(int),
                typeof(ExtendedEditor), 1);

        public int BorderWidth
        {
            get => (int)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }


        public ExtendedEditor()
        {

        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            var sizeRequest = base.OnMeasure(widthConstraint, heightConstraint);

            return new SizeRequest(new Size(sizeRequest.Request.Width, Math.Max(200, sizeRequest.Request.Height)));
        }
    }
}
