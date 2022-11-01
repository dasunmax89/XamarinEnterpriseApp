using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class ButtonListViewCell : ContentView
    {
        public ButtonListViewCell()
        {
            InitializeComponent();
            PropertyChanged += Cell_PropertyChanged;
        }

        private void Cell_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BindingContext))
            {
                var binding = BindingContext as WorkloadListItem;

                if (binding != null)
                {
                    Header1Label.IsVisible = !string.IsNullOrEmpty(binding.Header1);
                    Header2Label.IsVisible = !string.IsNullOrEmpty(binding.Header2);
                    Header3Label.IsVisible = !string.IsNullOrEmpty(binding.Header3);

                }
            }
        }
    }
}
