using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class HorizontalScrollView : ContentView
    {
        private int _currentIndex;

        private readonly IPlatformManager _platformManager;

        public static readonly BindableProperty DataSourceProperty =
        BindableProperty.Create("DataSource",
                              typeof(ObservableCollection<WorkloadListItem>),
                              typeof(HorizontalScrollView),
                              null,
                              BindingMode.OneWay,
                              propertyChanged: DataSourceProperty_Changed);

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

        public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create("SelectedItem",
                              typeof(WorkloadListItem),
                              typeof(HorizontalScrollView),
                              null,
                              BindingMode.OneWay,
                              propertyChanged: SelectedtemProperty_Changed);

        public WorkloadListItem SelectedItem
        {
            get
            {
                return (WorkloadListItem)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        public Type CellType { get; set; }

        private static void SelectedtemProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (HorizontalScrollView)bindable;

            if (newValue != null)
            {
                WorkloadListItem selectedItem = (WorkloadListItem)newValue;

                control.ScrollToItem(selectedItem);
            }
        }

        private static void DataSourceProperty_Changed(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (HorizontalScrollView)bindable;

            if (newValue != null)
            {
                control.RenderPage();
            }
        }

        private void RenderPage()
        {
            var displayInfo = DeviceDisplay.MainDisplayInfo;

            var deviceWidth = displayInfo.Width / displayInfo.Density;

            SetBusy(true);

            CarouselView.ItemsSource = DataSource;
            var itemTemplate = new DataTemplate(CellType);

            if (CellType == typeof(MarkerDetailView))
            {
                itemTemplate.SetValue(MarkerDetailView.NextCommandProperty, new Command(ScrollRight));
                itemTemplate.SetValue(MarkerDetailView.PrevCommandProperty, new Command(ScrollLeft));
                itemTemplate.SetValue(MarkerDetailView.CloseCommandProperty, new Command(OnClose));
                itemTemplate.SetValue(MarkerDetailView.FollowCommandProperty, new Command(OnAction));
            }

            CarouselView.ItemTemplate = itemTemplate;

            ScrollToItem(DataSource.First());

            SetBusy(false);
        }

        private void OnAction(object obj)
        {

        }

        private void OnClose(object obj)
        {

        }

        private void SetBusy(bool isBusy)
        {

        }

        public void ScrollToItem(WorkloadListItem sender)
        {
            if (sender != null)
            {
                CarouselView.ScrollTo(sender, position: ScrollToPosition.Center, animate: true);

                _platformManager.MarkAsRead(sender.IthdId, sender.CustId);
            }
        }

        public HorizontalScrollView()
        {
            InitializeComponent();

            var displayInfo = DeviceDisplay.MainDisplayInfo;

            double height = displayInfo.Height / (displayInfo.Density);

            if (height == 568)
            {
                CarouselView.HeightRequest = 400;
            }
            else
            {
                CarouselView.HeightRequest = 430;
            }

            _platformManager = DependencyService.Get<IPlatformManager>();

        }

        //private void Handle_Scrolled(object sender, ScrolledEventArgs e)
        //{
        //    double xProportion = e.ScrollX / ScrollView.ContentSize.Width;

        //    double indexProportion = DataSource.Count * xProportion;

        //    int index = (int)Math.Round(indexProportion, 0);

        //    if (index >= 0 && index < DataSource.Count)
        //    {
        //        var item = DataSource.ElementAt(index);

        //        if (item != null)
        //        {
        //            _currentIndex = index;
        //        }
        //    }
        //    else
        //    {
        //        if (index < 0)
        //        {
        //            _currentIndex = index;
        //        }
        //        else if (index >= DataSource.Count)
        //        {
        //            _currentIndex = DataSource.Count - 1;
        //        }
        //    }
        //}

        private void ScrollLeft(object obj)
        {
            WorkloadListItem args = obj as WorkloadListItem;

            int index = DataSource.IndexOf(args);

            if (index == 0)
            {
                return;
            }

            int toIndex = (index - 1);

            CarouselView.ScrollTo(toIndex, position: ScrollToPosition.Center, animate: true);
        }

        private void ScrollRight(object obj)
        {
            WorkloadListItem args = obj as WorkloadListItem;

            int index = DataSource.IndexOf(args);

            if (index >= DataSource.Count - 1)
            {
                return;
            }

            int toIndex = (index + 1);

            CarouselView.ScrollTo(toIndex, position: ScrollToPosition.Center, animate: true);
        }

        private void CarouselView_SizeChanged(object sender, EventArgs e)
        {
            var contentSize = ((VisualElement)sender).Height;
            if (contentSize > CarouselView.HeightRequest)
            {
                CarouselView.HeightRequest = contentSize;
            }
        }

        private void CarouselView_Scrolled(System.Object sender, ItemsViewScrolledEventArgs e)
        {
            if (DataSource?.Count == 1)
            {
                CarouselView.ScrollTo(0, position: ScrollToPosition.Center, animate: false);
            }
        }
    }
}
