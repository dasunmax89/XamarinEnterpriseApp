using System;
using System.Collections.Generic;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class HeaderView : ContentView
    {
        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
                                                  "TitleText",
                                                  typeof(string),
                                                  typeof(HeaderView),
                                                  "",
                                                  defaultBindingMode: BindingMode.OneWay);

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


        public static readonly BindableProperty SearchTextProperty = BindableProperty.Create(
                                                 "SearchText",
                                                 typeof(string),
                                                 typeof(HeaderView),
                                                 "",
                                                 defaultBindingMode: BindingMode.OneWay);

        public string SearchText
        {
            get
            {
                return (string)GetValue(SearchTextProperty);
            }
            set
            {
                SetValue(SearchTextProperty, value);
            }
        }

        public static readonly BindableProperty SearchCommandProperty =
        BindableProperty.Create("SearchCommand",
                          typeof(ICommand),
                          typeof(HeaderView),
                          null,
                          BindingMode.OneWay);

        public ICommand SearchCommand
        {
            get
            {
                return (ICommand)GetValue(SearchCommandProperty);
            }
            set
            {
                SetValue(SearchCommandProperty, value);
            }
        }

        public static readonly BindableProperty SearchFocusedCommandProperty =
        BindableProperty.Create("SearchFocusedCommand",
                  typeof(ICommand),
                  typeof(HeaderView),
                  null,
                  BindingMode.OneWay);

        public ICommand SearchFocusedCommand
        {
            get
            {
                return (ICommand)GetValue(SearchFocusedCommandProperty);
            }
            set
            {
                SetValue(SearchFocusedCommandProperty, value);
            }
        }

        public static readonly BindableProperty SearchTextChangedCommandProperty =
        BindableProperty.Create("SearchTextChangedCommand",
          typeof(ICommand),
          typeof(HeaderView),
          null,
          BindingMode.OneWay);

        public ICommand SearchTextChangedCommand
        {
            get
            {
                return (ICommand)GetValue(SearchTextChangedCommandProperty);
            }
            set
            {
                SetValue(SearchTextChangedCommandProperty, value);
            }
        }

        public string ButtonPanelVisibleItems
        {
            get
            {
                return (string)GetValue(ButtonPanelVisibleItemsProperty);
            }
            set
            {
                SetValue(ButtonPanelVisibleItemsProperty, value);
            }
        }

        public static readonly BindableProperty ButtonPanelVisibleItemsProperty = BindableProperty.Create(
                                                         "ButtonPanelVisibleItems",
                                                         typeof(string),
                                                         typeof(HeaderView),
                                                         string.Empty,
                                                         defaultBindingMode: BindingMode.OneWay);

        public string ImageIcon
        {
            get
            {
                return (string)GetValue(ImageIconProperty);
            }
            set
            {
                SetValue(ImageIconProperty, value);
            }
        }

        public static readonly BindableProperty ImageIconProperty = BindableProperty.Create(
                                                          "ImageIcon",
                                                          typeof(string),
                                                          typeof(HeaderView),
                                                          string.Empty,
                                                          defaultBindingMode: BindingMode.OneWay);
        public HeaderView()
        {
            InitializeComponent();

            PropertyChanged += Component_PropertyChanged;

            HeaderBar.IsVisible = true;
            SearchBar.IsVisible = false;
            LogoButton.IsVisible = false;

            SearchTextEntry.Keyboard = Keyboard.Create(KeyboardFlags.None);

            EvaluateBackButton();

            SetButtonPanel(ButtonPanelVisibleItems);
        }

        public void EvaluateBackButton()
        {
            INavigationService navigationService = DependencyResolver.Resolve<INavigationService>();

            if (navigationService.IsBackButtonVisible)
            {
                BackButton.Source = "Arrow_back.png";
            }
            else
            {
                BackButton.Source = "hamburger_menu.png";
            }
        }

        private void Component_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchCommand))
            {
                if (SearchCommand != null)
                {
                    if (!string.IsNullOrWhiteSpace(GlobalSetting.Instance.SearchString))
                    {
                        SearchTextEntry.Text = GlobalSetting.Instance.SearchString;
                        DisplaySearchBar(true);
                    }
                }
            }
            else if (e.PropertyName == nameof(ButtonPanelVisibleItems))
            {
                string enabledItems = ButtonPanelVisibleItems?.ToString();
                SetButtonPanel(enabledItems);
            }
            else if (e.PropertyName == nameof(TitleText))
            {
                bool isTitleAvailable = !string.IsNullOrEmpty(TitleText);

                if (isTitleAvailable)
                {
                    AppTitleLabel.IsVisible = isTitleAvailable;

                    LogoButton.IsVisible = !isTitleAvailable;

                    AppTitleLabel.Text = TitleText.ToString();
                }
            }
            else if (e.PropertyName == nameof(SearchText))
            {
                SearchTextEntry.Text = SearchText?.ToString();
            }
            else if (e.PropertyName == nameof(ImageIcon))
            {
                bool isIconAvailable = !string.IsNullOrEmpty(ImageIcon);

                AppTitleLabel.IsVisible = !isIconAvailable;
                LogoButton.IsVisible = isIconAvailable;

                if (isIconAvailable)
                {
                    AppTitleLabel.HorizontalOptions = LayoutOptions.End;
                    LogoButton.Source = ImageIcon.ToImageSource();
                }
            }
            else if (e.PropertyName == nameof(BindingContext))
            {

            }
        }

        private void SetButtonPanel(string enabledItems)
        {
            SearchButton.IsVisible = false;
            SettingsButton.IsVisible = false;
            DeleteButton.IsVisible = false;
            CameraButton.IsVisible = false;

            if (!string.IsNullOrEmpty(enabledItems))
            {
                string[] items = enabledItems.Split(',');

                foreach (var item in items)
                {
                    switch (item)
                    {
                        case "search":
                            SearchButton.IsVisible = true;
                            break;
                        case "settings":
                            SettingsButton.IsVisible = true;
                            break;
                        case "delete":
                            DeleteButton.IsVisible = true;
                            break;
                        case "camera":
                            CameraButton.IsVisible = true;
                            break;
                    }
                }
            }
        }

        private async void OnBackButtonTapped(object sender, EventArgs args)
        {
            if (SearchBar.IsVisible)
            {
                DisplaySearchBar(false);

                EditorEventArgs editorEventArgs = new EditorEventArgs()
                {
                    Tag = "HeaderSearch",
                    Text = string.Empty,
                };

                SearchCommand?.Execute(editorEventArgs);
            }
            else
            {
                var vm = BindingContext as ViewModelBase;

                await vm.NavigateToBack();
            }
        }

        private void OnSettingsButtonTapped(object sender, EventArgs args)
        {
            //TODO
        }

        private async void OnDeleteButtonTapped(object sender, EventArgs args)
        {
            var vm = BindingContext as ViewModelBase;

            await vm.OnDelete();
        }

        private async void OnCameraButtonTapped(object sender, EventArgs args)
        {
            var vm = BindingContext as ViewModelBase;

            await vm.DisplayCameraActionsheet();
        }

        private void OnSearchButtonTapped(object sender, EventArgs args)
        {
            DisplaySearchBar(true);
        }

        private void DisplaySearchBar(bool isVisible)
        {
            SearchBar.IsVisible = isVisible;
            BackButton.IsVisible = isVisible;
            HeaderBar.IsVisible = !isVisible;

            if (!isVisible)
            {
                SearchTextEntry.Text = string.Empty;
            }
        }

        private void SearchTextEntry_Completed(object sender, EventArgs e)
        {
            RoundedEntry control = sender as RoundedEntry;

            if (!string.IsNullOrEmpty(control.Text))
            {
                EditorEventArgs args = new EditorEventArgs()
                {
                    Tag = "HeaderSearch",
                    Text = control.Text?.Trim(),
                };

                SearchCommand?.Execute(args);
            }
        }

        void SearchTextEntry_Focused(System.Object sender, FocusEventArgs e)
        {
            RoundedEntry control = sender as RoundedEntry;

            EditorEventArgs args = new EditorEventArgs()
            {
                Tag = "HeaderSearch",
                Text = control.Text?.Trim(),
            };

            SearchFocusedCommand?.Execute(args);
        }

        void SearchTextEntry_TextChanged(System.Object sender, TextChangedEventArgs e)
        {
            RoundedEntry control = sender as RoundedEntry;

            EditorEventArgs args = new EditorEventArgs()
            {
                Tag = "HeaderSearch",
                Text = control.Text?.Trim(),
            };

            SearchTextChangedCommand?.Execute(args);
        }
    }
}
