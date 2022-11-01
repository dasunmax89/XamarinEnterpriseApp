using System;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XamarinEnterpriseApp.Xamarin.Core.Controls
{
    public class ExtendedPinDroid : Pin
    {
        public static readonly BindableProperty WorkloadItemProperty =
           BindableProperty.Create(nameof(WorkloadItem),
                                   typeof(WorkloadListItem), typeof(ExtendedPinDroid), null);

        public WorkloadListItem WorkloadItem
        {
            get => (WorkloadListItem)GetValue(WorkloadItemProperty);
            set => SetValue(WorkloadItemProperty, value);
        }

        public int PinCount { get; set; }
    }
}
