using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class PageIndicatorView : ContentView
    {
        public static readonly BindableProperty ProgressCountProperty =
        BindableProperty.Create("ProgressCount",
                              typeof(int),
                              typeof(PageIndicatorView),
                              0,
                              BindingMode.OneWay);

        public int ProgressCount
        {
            get
            {
                return (int)GetValue(ProgressCountProperty);
            }
            set
            {
                SetValue(ProgressCountProperty, value);
            }
        }

        public static readonly BindableProperty CurrentProgressProperty =
        BindableProperty.Create("CurrentProgress",
                              typeof(int),
                              typeof(PageIndicatorView),
                              0,
                              BindingMode.OneWay);

        public int CurrentProgress
        {
            get
            {
                return (int)GetValue(CurrentProgressProperty);
            }
            set
            {
                SetValue(CurrentProgressProperty, value);
            }
        }

        public PageIndicatorView()
        {
            InitializeComponent();

            PropertyChanged += PageIndicatorView_PropertyChanged;
        }

        private void PageIndicatorView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CurrentProgress))
            {
                SetChildren();
            }
            else if (e.PropertyName == nameof(ProgressCount))
            {
                SetChildren();
            }
        }

        private void SetChildren()
        {
            Container.Children.Clear();

            if (ProgressCount > 0)
            {
                for (int i = 1; i <= ProgressCount; i++)
                {
                    var image = new Image()
                    {
                        Margin = new Thickness(-5, 2),
                        BackgroundColor = Color.Transparent,
                        Aspect = Aspect.AspectFill,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        HeightRequest = 24,
                        WidthRequest = 24,
                    };

                    if (i <= CurrentProgress)
                        image.Source = "dot_dark.png";
                    else
                        image.Source = "dot_light.png";

                    Container.Children.Add(image);
                }
            }
        }
    }
}
