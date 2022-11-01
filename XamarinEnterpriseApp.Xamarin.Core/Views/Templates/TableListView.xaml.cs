using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class TableListView : ContentView
    {
        public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create("Caption",
                              typeof(string),
                              typeof(TableListView),
                              string.Empty,
                              BindingMode.OneWay);


        public static readonly BindableProperty DataSourceProperty =
        BindableProperty.Create("DataSource",
                          typeof(ObservableCollection<ListItemModel>),
                          typeof(TableListView),
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

        public string Caption
        {
            get
            {
                return (string)GetValue(CaptionProperty);
            }
            set
            {
                SetValue(CaptionProperty, value);
            }
        }

        public static readonly BindableProperty CustomHeightProperty =
        BindableProperty.Create("CustomHeight",
                          typeof(int),
                          typeof(TableListView),
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

        public static readonly BindableProperty CaptionStyleProperty =
        BindableProperty.Create("CaptionStyle",
                         typeof(Style),
                         typeof(TableListView),
                         null,
                         BindingMode.OneWay);

        public Style CaptionStyle
        {
            get
            {
                return (Style)GetValue(CaptionStyleProperty);
            }
            set
            {
                SetValue(CaptionStyleProperty, value);
            }
        }

        public TableListView()
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
            else if (e.PropertyName == nameof(Caption))
            {
                CaptionLabel.Text = Caption;

                CaptionLabel.IsVisible = !string.IsNullOrEmpty(Caption);
            }
            else if (e.PropertyName == nameof(CustomHeight))
            {
                if (CustomHeight > 0)
                {
                    Scroll.HeightRequest = CustomHeight;
                }
            }
            else if (e.PropertyName == nameof(CaptionStyle))
            {
                if (CaptionStyle != null)
                {
                    CaptionLabel.Style = CaptionStyle;
                }
            }
        }

        private void RenderItems(ObservableCollection<ListItemModel> itemsToRender)
        {
            ItemContainer.Children.Clear();

            if (itemsToRender != null)
            {
                foreach (var listItem in itemsToRender)
                {
                    TableListViewCell listItemView = new TableListViewCell();
                    listItemView.BindingContext = listItem;
                    ItemContainer.Children.Add(listItemView);
                }
            }
        }
    }
}
