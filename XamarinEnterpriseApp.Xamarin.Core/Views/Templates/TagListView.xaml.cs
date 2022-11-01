using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class TagListView : ContentView
    {
        public Style TagBarContainerStyle { get; set; }

        public Style TagButtonContainerStyle { get; set; }

        public Style TagButtonSelectedStyle { get; set; }

        public Style TagButtonNormalStyle { get; set; }

        public static readonly BindableProperty DataSourceProperty =
        BindableProperty.Create("DataSource",
                          typeof(ObservableCollection<ListItemModel>),
                          typeof(TagListView),
                          null,
                          BindingMode.OneWay);

        public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create("Caption",
                              typeof(string),
                              typeof(TagListView),
                              string.Empty,
                              BindingMode.OneWay);

        public static readonly BindableProperty ClickedCommandProperty =
        BindableProperty.Create("ClickedCommand",
                        typeof(ICommand),
                        typeof(TagListView),
                        null,
                        BindingMode.OneWay);

        public ICommand ClickedCommand
        {
            get
            {
                return (ICommand)GetValue(ClickedCommandProperty);
            }
            set
            {
                SetValue(ClickedCommandProperty, value);
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

        public TagListView()
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
            }
        }

        private void RenderItems(ObservableCollection<ListItemModel> dataSource)
        {
            ItemContainer.Children.Clear();

            var resources = Application.Current.Resources;

            if (dataSource != null)
            {
                foreach (var item in dataSource)
                {
                    TagListViewCell button = new TagListViewCell();

                    button.TagButtonNormalStyle = TagButtonNormalStyle;
                    button.TagButtonSelectedStyle = TagButtonSelectedStyle;
                    button.TagButtonContainerStyle = TagButtonContainerStyle;

                    button.SetStyle(false);

                    button.HorizontalOptions = LayoutOptions.FillAndExpand;

                    button.VerticalOptions = LayoutOptions.Center;

                    button.BindingContext = item;
                    button.SetBinding(TagListViewCell.IsSelectedCustomProperty, new Binding("IsSelected", source: item));
                    button.SetBinding(TagListViewCell.ClickedCommandProperty, new Binding("ClickedCommand", source: this));

                    ItemContainer.Children.Add(button);
                }
            }
        }

        private void SetTagBar(ListItemModel model, bool isActive)
        {
            model.SetSelected(true);

            if (isActive)
            {
                ClickedCommand?.Execute(model);
            }
        }
    }
}
