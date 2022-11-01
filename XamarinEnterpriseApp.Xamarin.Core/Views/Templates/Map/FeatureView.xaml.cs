using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class FeatureView : StackLayout
    {
        public FeatureView()
        {
            InitializeComponent();
        }

        public void SetProperties(GJFeature item, bool isSelected)
        {
            item.SetMarkerIcon(isSelected);

            BackgroundImage.Source = item.MarkerIconSource;

            MarkerLabel.Text = item.MarkerText;
        }
    }
}
