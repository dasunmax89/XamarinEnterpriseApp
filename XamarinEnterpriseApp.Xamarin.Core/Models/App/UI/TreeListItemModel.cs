using System;
using System.Collections.Generic;
using System.Linq;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class TreeListItemModel : ListItemModel
    {
        private Style _normalStyle;
        private Style _selectedStyle;

        public bool IsNodeItem { get; set; }

        public List<TreeListItemModel> Children { get; set; }

        public List<TreeListItemModel> FilteredChildren { get; set; }

        public TreeListItemModel ParentItem { get; set; }

        public int Level { get; set; }

        bool _isExpanded;
        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                _isExpanded = value;
                IconSource = value ? "Arrow_down.png" : "Arrow_right.png";
                HeaderStyle = value ? _selectedStyle : _normalStyle;
                RaisePropertyChanged(() => IsExpanded);
                RaisePropertyChanged(() => IconSource);
                RaisePropertyChanged(() => HeaderStyle);
            }
        }

        public bool Search(string text)
        {
            bool contains = false;

            bool isHeaderContains = Header.Search(text);

            if (isHeaderContains)
            {
                contains = isHeaderContains;
            }
            else
            {
                FilteredChildren = Children.FindAll(x => x.Search(text));

                contains = FilteredChildren.Any();
            }

            SetVisibility(contains, text);

            return contains;
        }

        public void SetVisibility(bool toVisible, string text = "")
        {
            if (toVisible)
            {
                IsExpanded = toVisible;

                var parentItem = ParentItem;

                while (parentItem != null)
                {
                    parentItem.IsExpanded = toVisible;
                    parentItem = parentItem.ParentItem;
                }
            }
            else
            {
                Children?.ForEach(y => y.IsExpanded = toVisible);
                IsExpanded = toVisible;
            }
        }

        public TreeListItemModel()
        {
            Children = new List<TreeListItemModel>();

            FilteredChildren = new List<TreeListItemModel>();

            _normalStyle = (Style)Application.Current.Resources["TreeViewListEntryHeaderStyle"];
            _selectedStyle = (Style)Application.Current.Resources["TreeViewListEntryHeaderSelectedStyle"];

            HeaderStyle = _normalStyle;
        }
    }
}
