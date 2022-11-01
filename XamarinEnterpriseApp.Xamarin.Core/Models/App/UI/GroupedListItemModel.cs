using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GroupedListItemModel : List<ListItemModel>
    {
        public string HeaderText { get; set; }

        public List<ListItemModel> Items { set { AddRange(value); } }
    }
}