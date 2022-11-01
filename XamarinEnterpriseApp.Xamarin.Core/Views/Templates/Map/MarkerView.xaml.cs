using System;
using System.Collections.Generic;
using System.IO;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class MarkerView : StackLayout
    {
        public MarkerView(WorkloadListItem item)
        {
            InitializeComponent();

            BindingContext = item;
        }

        public void SetProperties(bool isSelected)
        {
            WorkloadListItem item = BindingContext as WorkloadListItem;

            BackgroundImage.Source = item.MarkerIconSource;

            MarkerLabel.Text = item.MarkerText;
        }
    }
}
