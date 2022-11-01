using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class ButtonListView : ContentView
    {
        public static readonly BindableProperty DataSourceProperty =
        BindableProperty.Create("DataSource",
                          typeof(ObservableCollection<WorkloadListItem>),
                          typeof(ButtonListView),
                          null,
                          BindingMode.OneWay);

        public ObservableCollection<WorkloadListItem> DataSource
        {
            get
            {
                return (ObservableCollection<WorkloadListItem>)GetValue(DataSourceProperty);
            }
            set
            {
                SetValue(DataSourceProperty, value);
            }
        }

        public static readonly BindableProperty ItemTapCommandProperty =
        BindableProperty.Create("ItemTapCommand",
                          typeof(ICommand),
                          typeof(ButtonListView),
                          null,
                          BindingMode.OneWay);

        public ICommand ItemTapCommand
        {
            get
            {
                return (ICommand)GetValue(ItemTapCommandProperty);
            }
            set
            {
                SetValue(ItemTapCommandProperty, value);
            }
        }

        public ButtonListView()
        {
            InitializeComponent();
            PropertyChanged += Component_PropertyChanged;
        }

        private void Component_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DataSource))
            {
                RenderItems(DataSource);
            }
        }

        private void RenderItems(ObservableCollection<WorkloadListItem> itemsToRender)
        {
            ItemContainer.Children.Clear();

            if (itemsToRender != null)
            {
                foreach (var listItem in itemsToRender)
                {
                    ButtonListViewCell listItemView = new ButtonListViewCell();

                    var tapGestureRecognizer = new TapGestureRecognizer();

                    tapGestureRecognizer.Tapped += (s, e) =>
                    {
                        ButtonListViewCell tappedItem = s as ButtonListViewCell;

                        WorkloadListItem workloadListItem = tappedItem.BindingContext as WorkloadListItem;

                        ItemTapped(workloadListItem);
                    };

                    listItemView.GestureRecognizers.Add(tapGestureRecognizer);

                    listItemView.Margin = new Thickness(0, 5, 0, 5);

                    listItemView.BindingContext = listItem;
                    ItemContainer.Children.Add(listItemView);
                }
            }
        }

        private void ItemTapped(WorkloadListItem workloadListItem)
        {
            ItemTapCommand?.Execute(workloadListItem);
        }
    }
}
