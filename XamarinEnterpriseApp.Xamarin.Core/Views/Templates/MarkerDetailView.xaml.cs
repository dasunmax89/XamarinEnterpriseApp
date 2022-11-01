using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class MarkerDetailView : Grid
    {
        public static readonly BindableProperty CloseCommandProperty =
        BindableProperty.Create("CloseCommand",
                       typeof(ICommand),
                       typeof(MarkerDetailView),
                       null,
                       BindingMode.OneWay);

        public ICommand CloseCommand
        {
            get
            {
                return (ICommand)GetValue(CloseCommandProperty);
            }
            set
            {
                SetValue(CloseCommandProperty, value);
            }
        }


        public static readonly BindableProperty NextCommandProperty =
        BindableProperty.Create("NextCommand",
                       typeof(ICommand),
                       typeof(MarkerDetailView),
                       null,
                       BindingMode.OneWay);

        public ICommand NextCommand
        {
            get
            {
                return (ICommand)GetValue(NextCommandProperty);
            }
            set
            {
                SetValue(NextCommandProperty, value);
            }
        }

        public static readonly BindableProperty PrevCommandProperty =
       BindableProperty.Create("PrevCommand",
                      typeof(ICommand),
                      typeof(MarkerDetailView),
                      null,
                      BindingMode.OneWay);

        public ICommand PrevCommand
        {
            get
            {
                return (ICommand)GetValue(PrevCommandProperty);
            }
            set
            {
                SetValue(PrevCommandProperty, value);
            }
        }

        public static readonly BindableProperty FollowCommandProperty =
               BindableProperty.Create("FollowCommand",
                              typeof(ICommand),
                              typeof(MarkerDetailView),
                              null,
                              BindingMode.OneWay);

        public ICommand FollowCommand
        {
            get
            {
                return (ICommand)GetValue(FollowCommandProperty);
            }
            set
            {
                SetValue(FollowCommandProperty, value);
            }
        }

        public MarkerDetailView()
        {
            InitializeComponent();
            PropertyChanged += View_PropertyChanged;
        }

        private void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BindingContext))
            {
                WorkloadListItem workloadListItem = BindingContext as WorkloadListItem;

                if (workloadListItem != null && workloadListItem.ParentList != null)
                {
                    int index = workloadListItem.ParentList.IndexOf(workloadListItem) + 1;

                    SetIndex(index, workloadListItem.ParentList.Count);
                }
            }
        }

        private void FollowButton_Tapped(object sender, EventArgs e)
        {
            ImageButtonView imageButton = sender as ImageButtonView;

            if (imageButton.IsEnabledCustom)
            {
                WorkloadListItem args = BindingContext as WorkloadListItem;

                FollowCommand.Execute(args);
            }
        }

        private void CloseButton_Tapped(object sender, EventArgs e)
        {
            WorkloadListItem args = BindingContext as WorkloadListItem;

            CloseCommand.Execute(args);
        }

        private void ArrowButton_Clicked(object sender, EventArgs e)
        {
            if (sender == LeftButton)
            {
                PrevCommand?.Execute(BindingContext);
            }
            else if (sender == RightButton)
            {
                NextCommand?.Execute(BindingContext);
            }
        }

        public void SetIndex(int index, int count)
        {
            IndexLabel.Text = $"{index} {AppResources.Of} {count}";

            if (index == count)
            {
                RightButton.IsEnabled = false;

                RightButton.Source = "arrow_right_blue_disabled.png";
            }
            else
            {
                RightButton.IsEnabled = true;

                RightButton.Source = "arrow_right_blue.png";
            }

            if (index == 1)
            {
                LeftButton.IsEnabled = false;

                LeftButton.Source = "arrow_left_blue_disabled.png";
            }
            else
            {
                LeftButton.IsEnabled = true;

                LeftButton.Source = "arrow_left_blue.png";
            }

            if (count == 1)
            {
                NavigationContainer.IsVisible = false;
            }
            else
            {
                NavigationContainer.IsVisible = true;
            }
        }
    }
}
