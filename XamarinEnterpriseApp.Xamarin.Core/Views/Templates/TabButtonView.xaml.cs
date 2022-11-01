using System.ComponentModel;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class TabButtonView : ContentView
    {
        public Style TabButtonContainerStyle { get; set; }

        public Style TabButtonSelectedStyle { get; set; }

        public Style TabButtonNormalStyle { get; set; }

        public static readonly BindableProperty TabItemTappedCommandProperty =
         BindableProperty.Create("TabItemTappedCommand",
                              typeof(ICommand),
                              typeof(TabButtonView),
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

        public static readonly BindableProperty IsSelectedCustomProperty =
        BindableProperty.Create("IsSelected",
                               typeof(bool),
                               typeof(TabButtonView),
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

        public TabButtonView()
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

            TabButton.Style = isSelected ? TabButtonSelectedStyle : TabButtonNormalStyle;

            if (TabButtonContainerStyle != null)
            {
                Style = TabButtonContainerStyle;
            }

            string helpText = isSelected ? $"{listItem?.Header}.Status {AppResources.Active}" : listItem?.Header;

            TabButton.SetValue(AutomationProperties.HelpTextProperty, helpText);
        }

        private void TabButton_Clicked(System.Object sender, System.EventArgs e)
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
