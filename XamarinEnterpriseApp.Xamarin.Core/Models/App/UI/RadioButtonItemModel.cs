using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models.Base;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class RadioButtonItemModel : ExtendedBindableObject
    {
        public string Caption { get; set; }

        public object Tag { get; set; }

        public object Identifier { get; set; }

        bool _isSelected = false;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(() => ImageSource);
                RaisePropertyChanged(() => HelpText);
            }
        }

        public string ImageSource
        {
            get
            {
                var image = IsSelected ? "RadioButton_Checked.png" : "RadioButton_Unchecked.png";
                return image;
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

        public List<RadioButtonItemModel> ListItems { get; internal set; }

        public void SetSelected()
        {
            foreach (var listItem in ListItems)
            {
                if (listItem == this)
                {
                    listItem.IsSelected = true;
                }
                else
                {
                    listItem.IsSelected = false;
                }

                if (listItem.Tag != null)
                {
                    if (listItem.Tag is ISelectable)
                    {
                        ISelectable selectable = listItem.Tag as ISelectable;

                        selectable.IsSelected = listItem.IsSelected;
                    }
                }
            }
        }
    }
}

