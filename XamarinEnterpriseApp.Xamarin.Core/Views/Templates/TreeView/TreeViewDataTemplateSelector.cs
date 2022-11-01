using System;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public class TreeViewDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CollapsedNodeTemplate { get; set; }

        public DataTemplate ExpandedNodeTemplate { get; set; }

        public DataTemplate ListTemplate { get; set; }

        public TreeViewDataTemplateSelector()
        {
            CollapsedNodeTemplate = new DataTemplate(() =>
            {
                TreeViewNodeCell cell = new TreeViewNodeCell();
                return cell;
            });

            ExpandedNodeTemplate = new DataTemplate(() =>
            {
                SubTreeViewNodeItemCell cell = new SubTreeViewNodeItemCell();
                return cell;
            });

            ListTemplate = new DataTemplate(() =>
            {
                TreeViewListItemCell itemCell = new TreeViewListItemCell();

                var cell = new ViewCell { View = itemCell };

                return cell;
            });
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            TreeListItemModel listItemModel = item as TreeListItemModel;

            DataTemplate dataTemplate = SelectTamplate(listItemModel);

            return dataTemplate;
        }

        public DataTemplate SelectTamplate(TreeListItemModel listItemModel)
        {
            DataTemplate dataTemplate;

            if (listItemModel.IsNodeItem)
            {
                if (listItemModel.IsExpanded)
                {
                    dataTemplate = ExpandedNodeTemplate;
                }
                else
                {
                    dataTemplate = CollapsedNodeTemplate;
                }
            }
            else
            {
                dataTemplate = ListTemplate;
            }

            return dataTemplate;
        }
    }
}
