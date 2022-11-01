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
    public partial class TabBarView : ContentView
    {
        public Style TabBarContainerStyle { get; set; }

        public Style TabButtonContainerStyle { get; set; }

        public Style TabButtonSelectedStyle { get; set; }

        public Style TabButtonNormalStyle { get; set; }

        public static readonly BindableProperty SelectedTabIndexProperty =
        BindableProperty.Create("SelectedTabIndex",
                          typeof(int),
                          typeof(TabBarView),
                          0,
                          BindingMode.OneWay);

        public int SelectedTabIndex
        {
            get
            {
                return (int)GetValue(SelectedTabIndexProperty);
            }
            set
            {
                SetValue(SelectedTabIndexProperty, value);
            }
        }

        public static readonly BindableProperty TabItemTappedCommandProperty =
        BindableProperty.Create("TabItemTappedCommand",
                          typeof(ICommand),
                          typeof(TabBarView),
                          null,
                          BindingMode.OneWay);

        public ICommand TabItemTappedCommand
        {
            get
            {
                return (ICommand)GetValue(TabItemTappedCommandProperty);
            }
            set
            {
                SetValue(TabItemTappedCommandProperty, value);
            }
        }

        public static readonly BindableProperty DataSourceProperty =
        BindableProperty.Create("DataSource",
                             typeof(ObservableCollection<ListItemModel>),
                             typeof(TabBarView),
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

        public TabBarView()
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
            else if (e.PropertyName == nameof(SelectedTabIndex))
            {
                ListItemModel model = DataSource?.FirstOrDefault(x => (int)x.Identifier == SelectedTabIndex);

                if (model != null)
                {
                    SetTabBar(model, false);
                }
            }
        }

        private void RenderItems(ObservableCollection<ListItemModel> dataSource)
        {
            ButtonPanel.Children.Clear();

            var resources = Application.Current.Resources;

            if (TabBarContainerStyle != null)
            {
                ContinerFrame.Style = TabBarContainerStyle;
            }

            if (dataSource != null)
            {
                foreach (var item in dataSource)
                {
                    TabButtonView button = new TabButtonView();

                    button.TabButtonNormalStyle = TabButtonNormalStyle;
                    button.TabButtonSelectedStyle = TabButtonSelectedStyle;

                    button.TabButtonContainerStyle = TabButtonContainerStyle;

                    button.SetStyle(false);

                    button.HorizontalOptions = LayoutOptions.FillAndExpand;

                    button.VerticalOptions = LayoutOptions.CenterAndExpand;

                    button.BindingContext = item;
                    button.SetBinding(TabButtonView.IsSelectedCustomProperty, new Binding("IsSelected", source: item));
                    button.SetBinding(TabButtonView.TabItemTappedCommandProperty, new Binding("TabItemTappedCommand", source: this));

                    ButtonPanel.Children.Add(button);
                }
            }
        }

        private void SetTabBar(ListItemModel model, bool isActive)
        {
            if (model != null)
            {
                foreach (var item in model.Siblings)
                {
                    item.IsSelected = model == item;
                }
            }

            if (isActive)
            {
                TabItemTappedCommand?.Execute(model);
            }
        }
    }
}
