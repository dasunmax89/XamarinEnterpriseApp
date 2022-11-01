using System;
using System.ComponentModel;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class TagListViewCell : ContentView
    {
        public Style TagButtonContainerStyle { get; set; }

        public Style TagButtonSelectedStyle { get; set; }

        public Style TagButtonNormalStyle { get; set; }

        public static readonly BindableProperty ClickedCommandProperty =
        BindableProperty.Create("ClickedCommand",
                              typeof(ICommand),
                              typeof(TagListViewCell),
                              null,
                              BindingMode.OneWay);

        public ICommand TabItemTappedCommand
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

        public static readonly BindableProperty IsSelectedCustomProperty =
        BindableProperty.Create("IsSelected",
                               typeof(bool),
                               typeof(TagListViewCell),
                               false,
                               BindingMode.OneWay);

        public bool IsSelected
        {
            get
            {
                return (bool)GetValue(IsSelectedCustomProperty);
            }
            set
            {
                SetValue(IsSelectedCustomProperty, value);
            }
        }

        public TagListViewCell()
        {
            InitializeComponent();
            PropertyChanged += Component_PropertyChanged;
        }

        private void Component_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsSelected))
            {
                SetStyle(IsSelected);
            }
        }

        public void SetStyle(bool isSelected)
        {
            ListItemModel listItem = BindingContext as ListItemModel;

            TagButton.Style = isSelected ? TagButtonSelectedStyle : TagButtonNormalStyle;

            if (TagButtonContainerStyle != null)
            {
                Style = TagButtonContainerStyle;
            }
        }

        private void TagButton_Clicked(object sender, EventArgs e)
        {
            ListItemModel model = BindingContext as ListItemModel;

            if (model != null)
            {
                foreach (var item in model.Siblings)
                {
                    item.IsSelected = model == item;
                }
            }

            TabItemTappedCommand?.Execute(model);
        }
    }
}
