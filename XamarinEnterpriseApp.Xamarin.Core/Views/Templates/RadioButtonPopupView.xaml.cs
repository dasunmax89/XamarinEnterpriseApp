using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class RadioButtonPopupView : ContentView
    {
        public string TitleText
        {
            get
            {
                return (string)GetValue(TitleTextProperty);
            }
            set
            {
                SetValue(TitleTextProperty, value);
            }
        }

        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
                                                         "TitleText",
                                                         typeof(string),
                                                         typeof(RadioButtonPopupView),
                                                         "",
                                                         defaultBindingMode: BindingMode.OneWay,
                                                         propertyChanged: TitleTextPropertyChanged);

        public static readonly BindableProperty DataSourceProperty =
        BindableProperty.Create("DataSource",
                          typeof(ObservableCollection<RadioButtonItemModel>),
                          typeof(RadioButtonPopupView),
                          null,
                          BindingMode.OneWay);

        public ObservableCollection<RadioButtonItemModel> DataSource
        {
            get
            {
                return (ObservableCollection<RadioButtonItemModel>)GetValue(DataSourceProperty);
            }
            set
            {
                SetValue(DataSourceProperty, value);
            }
        }

        public static readonly BindableProperty SelectedCommandProperty =
        BindableProperty.Create("SelectedCommand",
                        typeof(ICommand),
                        typeof(RadioButtonPopupView),
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

        public static readonly BindableProperty IsListFilterVisibleProperty =
        BindableProperty.Create("IsListFilterVisible",
                  typeof(bool),
                  typeof(RadioButtonPopupView),
                  false,
                  BindingMode.OneWay);

        public bool IsListFilterVisible
        {
            get
            {
                return (bool)GetValue(IsListFilterVisibleProperty);
            }
            set
            {
                SetValue(IsListFilterVisibleProperty, value);
            }
        }

        public static readonly BindableProperty ListHeightProperty =
        BindableProperty.Create("ListHeight",
                  typeof(int),
                  typeof(RadioButtonPopupView),
                  0,
                  BindingMode.OneWay);

        public int ListHeight
        {
            get
            {
                return (int)GetValue(ListHeightProperty);
            }
            set
            {
                SetValue(ListHeightProperty, value);
            }
        }

        private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RadioButtonPopupView)bindable;
            control.TitleLabel.Text = newValue?.ToString();
        }

        public RadioButtonPopupView()
        {
            InitializeComponent();
            PropertyChanged += Component_PropertyChanged;
            SearchTextEntry.IsVisible = false;
        }

        private void Component_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DataSource))
            {
                SetListView(DataSource);
            }
            else if (e.PropertyName == nameof(IsListFilterVisible))
            {
                SearchTextEntry.IsVisible = IsListFilterVisible;
            }
            else if (e.PropertyName == nameof(ListHeight))
            {
                ListView.HeightRequest = ListHeight;
            }
            else if (e.PropertyName == nameof(IsVisible))
            {
                var view = Parent as Layout;

                if (IsVisible)
                {
                    view.RaiseChild(this);

                    TitleLabel.Focus();
                }
                else
                {
                    view.LowerChild(this);

                    TitleLabel.Unfocus();
                }
            }
        }

        private void SetListView(ObservableCollection<RadioButtonItemModel> dataSource)
        {
            ListView.ItemsSource = dataSource;

            if (dataSource != null)
            {
                var selectedItem = dataSource.FirstOrDefault(x => x.IsSelected);
                if (selectedItem != null)
                {
                    ListView.ScrollTo(selectedItem, ScrollToPosition.Center, false);
                }
            }
        }

        private void HandleLeftButton_Clicked(object sender, System.EventArgs e)
        {
            HidePopup();
        }

        private async void HandleRightButton_Clicked(object sender, System.EventArgs e)
        {
            var vm = BindingContext as ViewModelBase;

            var selectedItem = vm.PopupListItems.FirstOrDefault(x => x.IsSelected);

            IDialogService dialogService = DependencyResolver.Resolve<IDialogService>();

            if (selectedItem != null)
            {
                HidePopup();

                vm.SelectListOkCommand?.Execute(selectedItem);
            }
            else
            {
                await dialogService.ShowDialog(
                    AppResources.SelectItemToContinue,
                    AppResources.AppName,
                    AppResources.OK);
            }
        }

        private void HidePopup()
        {
            var vm = BindingContext as ViewModelBase;
            vm.IsPopupVisible = false;
            SearchTextEntry.Text = string.Empty;
        }

        private void RoundedEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataSource != null)
            {
                var vm = BindingContext as ViewModelBase;

                var searchString = e.NewTextValue;

                var filteredList = DataSource.Where(x => string.IsNullOrEmpty(searchString) || x.Caption.Search(searchString)).ToObservableCollection();

                SetListView(filteredList);
            }
        }
    }
}
