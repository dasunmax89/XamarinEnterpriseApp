using System;
using System.Collections.Generic;
using System.Linq;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class SubTreeViewNodeItemCell : ExtendedViewCell
    {
        public static readonly BindableProperty DataSourceProperty =
        BindableProperty.Create("DataSource",
                             typeof(List<TreeListItemModel>),
                             typeof(SubTreeViewNodeItemCell),
                             null,
                             BindingMode.OneWay);

        public List<TreeListItemModel> DataSource
        {
            get
            {
                return (List<TreeListItemModel>)GetValue(DataSourceProperty);
            }
            set
            {
                SetValue(DataSourceProperty, value);
            }
        }

        private void Tree_ItemTapped(object sender, EventArgs args)
        {
            var listItem = BindingContext as TreeListItemModel;

            MessagingCenter.Send(GlobalSetting.Instance, Core.Constants.MessageKeys.TreeNodeTapped, listItem);
        }

        public SubTreeViewNodeItemCell()
        {
            InitializeComponent();
            PropertyChanged += Cell_PropertyChanged;
        }

        private void Cell_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DataSource))
            {
                var listItem = BindingContext as TreeListItemModel;

                if (listItem != null)
                {
                    List<TreeListItemModel> listItems = new List<TreeListItemModel>();

                    foreach (var subNode in listItem.FilteredChildren)
                    {
                        listItems.Add(subNode);

                        if (subNode.IsExpanded)
                        {
                            listItems.AddRange(subNode.FilteredChildren);
                        }
                    }

                    RenderItems(listItems);
                }
            }
        }

        private void RenderItems(List<TreeListItemModel> itemsToRender)
        {
            foreach (var listItem in itemsToRender)
            {
                ContentView listItemView = SelectTamplate(listItem);

                ItemContainer.Children.Add(listItemView);
            }
        }

        public ContentView SelectTamplate(TreeListItemModel listItemModel)
        {
            ContentView dataTemplate;

            if (listItemModel.IsNodeItem)
            {
                dataTemplate = new TreeViewHeaderCell();
            }
            else
            {
                dataTemplate = new TreeViewListItemCell();
            }

            //dataTemplate.Margin = new Thickness(1);
            //dataTemplate.Padding = new Thickness(0,0,0,5);
            //dataTemplate.HorizontalOptions = LayoutOptions.Center;
            //dataTemplate.VerticalOptions = LayoutOptions.Center;
            dataTemplate.BindingContext = listItemModel;

            return dataTemplate;
        }
    }
}
