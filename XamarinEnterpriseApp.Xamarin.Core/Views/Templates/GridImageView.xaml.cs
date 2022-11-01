using System;
using System.Collections.Generic;
using System.ComponentModel;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class GridImageView : ContentView
    {


        public static readonly BindableProperty DeleteButtonVisibleProperty = BindableProperty.Create(
                                                         "DeleteButtonVisible",
                                                         typeof(bool),
                                                         typeof(GridImageView),
                                                         true,
                                                         defaultBindingMode: BindingMode.OneWay);

        public bool DeleteButtonVisible
        {
            get
            {
                return (bool)GetValue(DeleteButtonVisibleProperty);
            }
            set
            {
                SetValue(DeleteButtonVisibleProperty, value);
            }
        }

        public GridImageView()
        {
            InitializeComponent();

            PropertyChanged += Component_PropertyChanged;
        }

        private void Component_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DeleteButtonVisible))
            {
                DeleteImageButtonLayout.IsVisible = DeleteButtonVisible;
            }
        }

        private void Delete_ButtonClcked(object sender, System.EventArgs e)
        {
            var item = BindingContext as GalleryListItemModel;

            MessagingCenter.Send(GlobalSetting.Instance, Constants.MessageKeys.GalleryItemTapped, item);
        }
    }
}
