using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class VerticalListView : ContentView
    {
        public Type CellType { get; set; }

        public static readonly BindableProperty SelectedCommandProperty =
        BindableProperty.Create("SelectedCommand",
                      typeof(ICommand),
                      typeof(VerticalListView),
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

        public static readonly BindableProperty DataSourceProperty =
        BindableProperty.Create("DataSource",
                          typeof(ObservableCollection<ListItemModel>),
                          typeof(VerticalListView),
                          null,
                          BindingMode.OneWay);

        public ObservableCollection<ListItemModel> DataSource
        {
            get
            {
                return (ObservableCollection<ListItemModel>)GetValue(DataSourceProperty);
            }
            set
            {
                SetValue(DataSourceProperty, value);
            }
        }

        public static readonly BindableProperty CustomHeightProperty =
        BindableProperty.Create("CustomHeight",
                          typeof(int),
                          typeof(VerticalListView),
                          null,
                          BindingMode.OneWay);

        public int CustomHeight
        {
            get
            {
                return (int)GetValue(CustomHeightProperty);
            }
            set
            {
                SetValue(CustomHeightProperty, value);
            }
        }

        public VerticalListView()
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
            else if (e.PropertyName == nameof(IsVisible))
            {
                if (IsVisible)
                {
                    RenderItems(DataSource);
                }
            }
            else if (e.PropertyName == nameof(CustomHeight))
            {
                ItemScrollView.HeightRequest = CustomHeight;
            }
        }

        private void RenderItems(ObservableCollection<ListItemModel> itemsToRender)
        {
            ItemContainer.Children.Clear();

            Type cellType = CellType;

            if (itemsToRender != null)
            {
                foreach (var listItem in itemsToRender)
                {
                    View cell = Activator.CreateInstance(cellType) as View;

                    cell.BindingContext = listItem;

                    ICollectionViewCell collectionViewCell = cell as ICollectionViewCell;

                    cell.Margin = new Thickness(5, 1);

                    cell.SetBinding(collectionViewCell.GetSelectedCommandProperty(), new Binding("SelectedCommand", source: this));

                    cell.SetBinding(collectionViewCell.GetDeletedCommandProperty(), new Binding("DeletedCommand", source: this));

                    ItemContainer.Children.Add(cell);
                }
            }
        }
    }
}
