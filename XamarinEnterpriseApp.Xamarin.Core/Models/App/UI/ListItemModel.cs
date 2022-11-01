using System;
using System.Collections.Generic;
using System.Linq;
using XamarinEnterpriseApp.Xamarin.Core.Enums;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models.Base;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ListItemModel : ExtendedBindableObject, ISelectable
    {
        string _caption;
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                RaisePropertyChanged(() => HelpText);
                RaisePropertyChanged(() => Caption);
            }
        }

        public string Header { get; set; }

        public string SubHeader { get; set; }

        public string VisibleItems { get; set; }

        public Type ViewModel { get; set; }

        public object Tag { get; set; }

        public Type CellType { get; set; }

        public object Identifier { get; set; }

        public List<ListItemModel> Siblings { get; set; }

        public bool IsAccessibilityOn { get; set; }

        string _iconSource;
        public string IconSource
        {
            get
            {
                return _iconSource;
            }
            set
            {
                _iconSource = value;
                RaisePropertyChanged(() => IconSource);
            }
        }

        Style _headerStyle;
        public Style HeaderStyle
        {
            get
            {
                return _headerStyle;
            }
            set
            {
                _headerStyle = value;
                RaisePropertyChanged(() => HeaderStyle);
            }
        }

        Style _detailStyle;
        public Style DetailStyle
        {
            get
            {
                return _detailStyle;
            }
            set
            {
                _detailStyle = value;
                RaisePropertyChanged(() => DetailStyle);
            }
        }

        string _extraIconSource;
        public string ExtraIconSource
        {
            get
            {
                return _extraIconSource;
            }
            set
            {
                _extraIconSource = value;
                RaisePropertyChanged(() => ExtraIconSource);
            }
        }

        bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(() => IsSelected);
                RaisePropertyChanged(() => HelpText);
            }
        }

        public string HelpText
        {
            get
            {
                string selectedText = IsSelected ? AppResources.Checked : AppResources.UnChecked;

                string helpText = $"{Caption}.{selectedText}";

                return helpText;
            }
        }

        public ListItemModel()
        {
            Siblings = new List<ListItemModel>();
        }

        public void SetSelected(bool isSelected)
        {
            if (Siblings != null)
            {
                foreach (var item in Siblings)
                {
                    item.IsSelected = false;
                }
            }

            IsSelected = isSelected;
        }
    }
}

