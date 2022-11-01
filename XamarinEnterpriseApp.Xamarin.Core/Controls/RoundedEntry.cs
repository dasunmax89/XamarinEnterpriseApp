using System;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Controls
{
    public class RoundedEntry : Entry, INavigatableInput
    {
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor),
                typeof(Color), typeof(RoundedEntry), Color.Gray);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(int),
                typeof(RoundedEntry), 1);

        public int BorderWidth
        {
            get => (int)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius),
                typeof(int), typeof(RoundedEntry), 0);

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty CustomPaddingProperty =
            BindableProperty.Create(nameof(CustomPadding),
                typeof(int), typeof(RoundedEntry), 0);

        public int CustomPadding
        {
            get => (int)GetValue(CustomPaddingProperty);
            set => SetValue(CustomPaddingProperty, value);
        }

        public static readonly BindableProperty IsCurvedCornersEnabledProperty =
            BindableProperty.Create(nameof(IsCurvedCornersEnabled),
                typeof(bool), typeof(RoundedEntry), true);

        public bool IsCurvedCornersEnabled
        {
            get => (bool)GetValue(IsCurvedCornersEnabledProperty);
            set => SetValue(IsCurvedCornersEnabledProperty, value);
        }

        public static readonly BindableProperty NextTextFieldProperty =
            BindableProperty.Create(nameof(NextTextField),
                                    typeof(InputView), typeof(RoundedEntry), null);

        public InputView NextTextField
        {
            get => (InputView)GetValue(NextTextFieldProperty);
            set => SetValue(NextTextFieldProperty, value);
        }

        public RoundedEntry()
        {

        }
    }
}
