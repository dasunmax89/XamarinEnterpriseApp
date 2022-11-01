using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class ShowcaseView : ContentView
    {
        private readonly IPlatformManager _platformManager;

        public static readonly BindableProperty DataSourceProperty =
        BindableProperty.Create("DataSource",
                           typeof(ObservableCollection<GalleryListItemModel>),
                           typeof(ShowcaseView),
                           null,
                           BindingMode.OneWay);

        public static readonly BindableProperty ClickedCommandProperty =
        BindableProperty.Create("ClickedCommand",
                          typeof(ICommand),
                          typeof(ShowcaseView),
                          null,
                          BindingMode.OneWay);

        public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create("SelectedItem",
                         typeof(GalleryListItemModel),
                         typeof(ShowcaseView),
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

        public GalleryListItemModel SelectedItem
        {
            get
            {
                return (GalleryListItemModel)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

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

        public int CurrentIndex { get; set; }

        public ShowcaseView()
        {
            InitializeComponent();

            _platformManager = DependencyService.Get<IPlatformManager>();

            PropertyChanged += View_PropertyChanged;

        }

        private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DataSource))
            {
                SetDataSource();
            }
            else if (e.PropertyName == nameof(SelectedItem))
            {
                SelectImage(SelectedItem);
            }
        }

        private void SetDataSource()
        {
            DotContainer.Children.Clear();

            if (DataSource.Any())
            {
                GalleryListItemModel firstItem = DataSource.FirstOrDefault();

                SelectImage(firstItem);
            }
        }

        private void SelectImage(GalleryListItemModel galleryItem)
        {
            if (galleryItem != null)
            {
                CurrentIndex = DataSource.IndexOf(galleryItem);

                ImageView.Source = galleryItem.ImageSource;
                ImageView.BindingContext = galleryItem;

                var page = Application.Current.MainPage;

                var color = Color.FromHex(galleryItem.BackgroundColor);

                page.BackgroundColor = color;
                BackgroundColor = color;
                _platformManager.SetStatusBarColor(color);


                TitleLabel.Text = galleryItem.Header;
                TextLabel.Text = galleryItem.SubHeader;

                TitleLabel.TextColor = Color.FromHex(galleryItem.TitleColor);
                TextLabel.TextColor = Color.FromHex(galleryItem.TextColor);

                AutomationProperties.SetHelpText(ImageView, galleryItem.ImageAcc);
                AutomationProperties.SetName(ImageView, AppResources.ImageAcc);

                DrawDots(DataSource);

                SkipButton.IsVisible = CurrentIndex == 0;
                GetStartedButton.IsVisible = CurrentIndex == DataSource.Count() - 1;

                if (CurrentIndex == 0)
                {
                    CloseButton.Source = "forwardButton.png";
                    CloseButton.WidthRequest = 9;
                    CloseButton.HeightRequest = 18;

                }
                else
                {
                    CloseButton.Source = "closeButton.png";
                    CloseButton.WidthRequest = 24;
                    CloseButton.HeightRequest = 24;
                }
            }
        }

        private void DrawDots(ObservableCollection<GalleryListItemModel> dataSource)
        {
            DotContainer.Children.Clear();

            int i = 0;

            foreach (var item in dataSource)
            {
                var image = new Image()
                {
                    BackgroundColor = Color.Transparent,
                    Aspect = Aspect.AspectFill,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    HeightRequest = 6,
                    WidthRequest = 6,
                    BindingContext = item,
                    Margin = new Thickness(7, 0)
                };

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) =>
                {
                    Image tappedImage = s as Image;

                    GalleryListItemModel galleryItem = tappedImage.BindingContext as GalleryListItemModel;

                    SelectImage(galleryItem);
                };

                image.GestureRecognizers.Add(tapGestureRecognizer);

                if (i == CurrentIndex)
                    image.Source = "dot_light.png";
                else
                    image.Source = "dot_dark.png";

                DotContainer.Children.Add(image);

                i++;
            }
        }

        private void Delete_ButtonClcked(object sender, System.EventArgs e)
        {
            if (DataSource != null)
            {
                GalleryListItemModel item = DataSource[CurrentIndex];

                MessagingCenter.Send(GlobalSetting.Instance, Constants.MessageKeys.GalleryItemTapped, item);
            }
        }

        private void Close_ButtonClcked(object sender, System.EventArgs e)
        {
            if (CurrentIndex == 0)
            {
                OnSwiped(this, new SwipedEventArgs(this, SwipeDirection.Left));
            }
            else
            {
                ExitIntro();
            }
        }

        private async void ExitIntro()
        {
            var navigationService = DependencyResolver.Resolve<INavigationService>();

            var settingsService = DependencyResolver.Resolve<ISettingsService>();

            //settingsService.HasIntroShown = true;

            await navigationService.NavigateToAsync<MainViewModel>();
        }

        protected void OnSwiped(object sender, SwipedEventArgs e)
        {
            if (e.Direction == SwipeDirection.Left)
            {
                int nextIndex = CurrentIndex + 1;

                if (nextIndex < DataSource.Count)
                {
                    GalleryListItemModel nextitem = DataSource[nextIndex];

                    SelectImage(nextitem);
                }
            }
            else if (e.Direction == SwipeDirection.Right)
            {
                int prevIndex = CurrentIndex - 1;

                if (prevIndex >= 0)
                {
                    GalleryListItemModel nextitem = DataSource[prevIndex];

                    SelectImage(nextitem);
                }
            }
            else if (e.Direction == SwipeDirection.Down)
            {

            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Image tappedImage = sender as Image;

            GalleryListItemModel galleryItem = tappedImage.BindingContext as GalleryListItemModel;

            ClickedCommand?.Execute(galleryItem);
        }

        private void GetStartedButton_Clicked(System.Object sender, System.EventArgs e)
        {
            ExitIntro();
        }
    }
}

