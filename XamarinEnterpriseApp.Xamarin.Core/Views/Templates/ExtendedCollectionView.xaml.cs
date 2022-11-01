using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class ExtendedCollectionView : ContentView
    {
        public Type CellType { get; set; }

        public Type AlternatingCellType { get; set; }

        public static readonly BindableProperty SpanProperty =
        BindableProperty.Create("Span",
                         typeof(int),
                         typeof(ExtendedCollectionView),
                         0,
                         BindingMode.OneWay);

        public int Span
        {
            get
            {
                return (int)GetValue(SpanProperty);
            }
            set
            {
                SetValue(SpanProperty, value);
            }
        }

        public static readonly BindableProperty SelectedCommandProperty =
        BindableProperty.Create("SelectedCommand",
                         typeof(ICommand),
                         typeof(ExtendedCollectionView),
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
                       typeof(ExtendedCollectionView),
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


        public static readonly BindableProperty DataSourceProperty =
        BindableProperty.Create("DataSource",
                             typeof(ObservableCollection<GalleryListItemModel>),
                             typeof(ExtendedCollectionView),
                             null,
                             BindingMode.OneWay);

        public ObservableCollection<GalleryListItemModel> DataSource
        {
            get
            {
                return (ObservableCollection<GalleryListItemModel>)GetValue(DataSourceProperty);
            }
            set
            {
                SetValue(DataSourceProperty, value);
            }
        }

        public ExtendedCollectionView()
        {
            InitializeComponent();

            PropertyChanged += Component_PropertyChanged;

            WidthRequest = GlobalSetting.Instance.DeviceWidth;
        }

        private void Component_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DataSource))
            {
                RenderGrid(DataSource);
            }
        }

        private void RenderGrid(IEnumerable<GalleryListItemModel> dataSource)
        {
            if (dataSource == null)
            {
                return;
            }

            List<GalleryListItemModel> dataSourceToRender = dataSource.ToList();

            ItemContainer.Children.Clear();

            int rowIndex = 0;

            while (dataSourceToRender.Any())
            {
                Type cellType = CellType;

                if (AlternatingCellType != null)
                {
                    cellType = rowIndex % 2 == 0 ? CellType : AlternatingCellType;
                }

                List<GalleryListItemModel> currentRow = dataSourceToRender.Take(Span).ToList();

                var holder = new Grid();

                int columnIndex = 0;

                for (int i = 0; i < Span; i++)
                {
                    holder.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                    columnIndex++;
                }

                foreach (var item in currentRow)
                {
                    View cell = Activator.CreateInstance(cellType) as View;

                    cell.BindingContext = item;

                    ICollectionViewCell collectionViewCell = cell as ICollectionViewCell;

                    cell.Margin = new Thickness(5, 5);

                    cell.SetBinding(collectionViewCell.GetSelectedCommandProperty(), new Binding("SelectedCommand", source: this));

                    cell.SetBinding(collectionViewCell.GetDeletedCommandProperty(), new Binding("DeletedCommand", source: this));

                    holder.Children.Add(cell, currentRow.IndexOf(item), 0);

                    dataSourceToRender.Remove(item);
                }

                ItemContainer.Children.Add(holder);

                rowIndex++;
            }
        }
    }
}
