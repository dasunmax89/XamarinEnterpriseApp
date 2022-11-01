using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class GridLayoutView : ContentView
    {
        private readonly int _numColumns;

        public static readonly BindableProperty DataSourceProperty =
        BindableProperty.Create("DataSource",
                             typeof(ObservableCollection<GalleryListItemModel>),
                             typeof(GridLayoutView),
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

        public static readonly BindableProperty ImageDeleteButtonVisibleProperty = BindableProperty.Create(
                                                 "ImageDeleteButtonVisible",
                                                 typeof(bool),
                                                 typeof(GridLayoutView),
                                                 true,
                                                 defaultBindingMode: BindingMode.OneWay);

        public bool ImageDeleteButtonVisible
        {
            get
            {
                return (bool)GetValue(ImageDeleteButtonVisibleProperty);
            }
            set
            {
                SetValue(ImageDeleteButtonVisibleProperty, value);
            }
        }
        public GridLayoutView()
        {
            InitializeComponent();

            PropertyChanged += Component_PropertyChanged;

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                _numColumns = 4;
            }
            else
            {
                _numColumns = 2;
            }
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
            List<GalleryListItemModel> dataSourceToRender = dataSource.ToList();

            ItemContainer.Children.Clear();

            while (dataSourceToRender.Any())
            {
                List<GalleryListItemModel> currentRow = dataSourceToRender.Take(_numColumns).ToList();

                var holder = new Grid();

                int columnIndex = 0;

                for (int i = 0; i < _numColumns; i++)
                {
                    holder.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                    columnIndex++;
                }

                foreach (var item in currentRow)
                {
                    GridImageView imageView = new GridImageView();

                    imageView.BindingContext = item;

                    imageView.DeleteButtonVisible = ImageDeleteButtonVisible;

                    holder.Children.Add(imageView, currentRow.IndexOf(item), 0);

                    dataSourceToRender.Remove(item);
                }

                ItemContainer.Children.Add(holder);
            }
        }
    }
}
