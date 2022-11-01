using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class TreeViewListItemCell : ContentView
    {
        public static readonly BindableProperty IsSelectedCustomProperty =
        BindableProperty.Create("IsSelectedCustom",
                               typeof(bool),
                               typeof(TreeViewListItemCell),
                               false,
                               BindingMode.TwoWay);

        public bool IsSelectedCustom
        {
            get
            {
                return (bool)GetValue(IsSelectedCustomProperty);
            }
            set
            {
                SetValue(IsSelectedCustomProperty, value);
            }
        }

        public TreeViewListItemCell()
        {
            InitializeComponent();
            PropertyChanged += Cell_PropertyChanged;
            SetProperties(false);
        }

        private void SetProperties(bool isSelected)
        {
            ImageView.Source = isSelected ? "CheckMark_blue.png" : string.Empty;
            ListItemLable.Style = isSelected ? (Style)Application.Current.Resources["CommonListLabelSelectedHeaderStyle"] : (Style)Application.Current.Resources["CommonListLabelHeaderStyle"];
        }

        private void Tree_ItemTapped(object sender, EventArgs args)
        {
            var listItem = BindingContext as TreeListItemModel;

            MessagingCenter.Send(GlobalSetting.Instance, Core.Constants.MessageKeys.TreeItemTapped, listItem);
        }

        private void Cell_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsSelectedCustom))
            {
                var listItem = BindingContext as TreeListItemModel;

                if (listItem != null)
                {
                    SetProperties(listItem.IsSelected);
                }
            }
        }
    }
}
