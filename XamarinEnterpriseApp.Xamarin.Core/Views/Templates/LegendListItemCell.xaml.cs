using System;
using System.Collections.Generic;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class LegendListItemCell : ContentView, ICollectionViewCell
    {
        public static readonly BindableProperty SelectedCommandProperty =
        BindableProperty.Create("SelectedCommand",
                    typeof(ICommand),
                    typeof(LegendListItemCell),
                    null,
                    BindingMode.OneWay);

        public ICommand SelectedCommand
        {
            get
            {
                return (ICommand)GetValue(SelectedCommandProperty);
            }
            set
            {
                SetValue(SelectedCommandProperty, value);
            }
        }

        public static readonly BindableProperty DeletedCommandProperty =
        BindableProperty.Create("DeletedCommand",
                   typeof(ICommand),
                   typeof(LegendListItemCell),
                   null,
                   BindingMode.OneWay);

        public ICommand DeletedCommand
        {
            get
            {
                return (ICommand)GetValue(DeletedCommandProperty);
            }
            set
            {
                SetValue(DeletedCommandProperty, value);
            }
        }

        public LegendListItemCell()
        {
            InitializeComponent();
        }

        public BindableProperty GetSelectedCommandProperty()
        {
            return SelectedCommandProperty;
        }

        public BindableProperty GetDeletedCommandProperty()
        {
            return DeletedCommandProperty;
        }

        private void Selected_ButtonClcked(object sender, System.EventArgs e)
        {
            var item = BindingContext as ListItemModel;

            SelectedCommand?.Execute(item);
        }

        private void Deleted_ButtonClcked(object sender, System.EventArgs e)
        {
            var item = BindingContext as ListItemModel;

            DeletedCommand?.Execute(item);
        }
    }
}
