using System;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class TreeViewHeaderCell : ContentView
    {
        public TreeViewHeaderCell()
        {
            InitializeComponent();
        }

        private void Tree_ItemTapped(object sender, EventArgs args)
        {
            var listItem = BindingContext as TreeListItemModel;

            MessagingCenter.Send(GlobalSetting.Instance, Core.Constants.MessageKeys.TreeNodeTapped, listItem);
        }
    }
}
