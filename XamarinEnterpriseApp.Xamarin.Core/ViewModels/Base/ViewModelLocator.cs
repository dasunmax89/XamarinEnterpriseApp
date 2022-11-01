using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base
{
    public static class ViewModelLocator
    {
        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        public static bool UseMockService { get; set; }

        static ViewModelLocator()
        {

        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;

            if (view == null)
            {
                return;
            }

            var viewModel = DependencyResolver.ResolveViewModel(view);
            view.BindingContext = viewModel;
        }
    }
}