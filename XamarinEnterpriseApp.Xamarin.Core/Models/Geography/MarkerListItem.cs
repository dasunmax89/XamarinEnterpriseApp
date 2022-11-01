using System;
using System.Diagnostics;
using System.IO;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class MarkerListItem : ExtendedBindableObject
    {
        public string MarkerIconSource { get; set; }

        public bool IsImageMarkerIcon { get; set; }

        public StateTypeIcon StateIconSource { get; set; }

        public virtual string MarkerText
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual void SetMarkerIcon(bool isSelected)
        {

        }
    }
}
