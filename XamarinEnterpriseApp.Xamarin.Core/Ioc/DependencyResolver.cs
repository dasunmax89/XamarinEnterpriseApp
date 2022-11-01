using System;
using System.Globalization;
using System.Reflection;
using Autofac;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Repositories;
using XamarinEnterpriseApp.Xamarin.Core.Serialization;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.Services.Mocks;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Ioc
{
    public static class DependencyResolver
    {
        private static IContainer _container;

        public static void RegisterComponents(bool isMock = false)
        {
            var builder = new ContainerBuilder();

            var assembly = Assembly.GetExecutingAssembly();

            // View models
            builder.RegisterAssemblyTypes(assembly)
                  .Where(t => t.Name.EndsWith("ViewModel"));

            // Providers
            builder.RegisterType<RequestProvider>().As<IRequestProvider>();

            // Persistance
            // Persistance
            builder.RegisterType<LocalDbContextService>().As<ILocalDbContextService>()
                .WithParameter(new TypedParameter(typeof(bool), false)).SingleInstance();

            // Services
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<ApplicationDataService>().As<IApplicationDataService>();

            if (isMock)
            {
                builder.RegisterType<SettingsMockService>().As<ISettingsService>();
                builder.RegisterType<DialogMockService>().As<IDialogService>();
                builder.RegisterType<NavigationMockService>().As<INavigationService>();
                builder.RegisterType<PhoneMockService>().As<IPhoneService>();
                builder.RegisterType<UIRenderMockService>().As<IUIRenderService>();
                builder.RegisterType<ConnectionMockService>().As<IConnectionService>();
                builder.RegisterType<AnalyticsMockService>().As<IAnalyticsService>();
                builder.RegisterType<GeoLocationMockService>().As<IGeoLocationService>();
                builder.RegisterType<FileSystemMockService>().As<IFileSystemService>();
                builder.RegisterType<FileHelperMock>().As<IFileHelper>();
            }
            else
            {
                builder.RegisterType<SettingsService>().As<ISettingsService>();
                builder.RegisterType<DialogService>().As<IDialogService>();
                builder.RegisterType<NavigationService>().As<INavigationService>();
                builder.RegisterType<PhoneService>().As<IPhoneService>();
                builder.RegisterType<UIRenderService>().As<IUIRenderService>();
                builder.RegisterType<ConnectionService>().As<IConnectionService>();
                builder.RegisterType<AnalyticsService>().As<IAnalyticsService>();
                builder.RegisterType<GeoLocationService>().As<IGeoLocationService>();
                builder.RegisterType<FileSystemService>().As<IFileSystemService>();
                builder.RegisterInstance<IFileHelper>(DependencyService.Get<IFileHelper>());

            }

            _container = builder.Build();
        }

        public static object Resolve(Type resolveType)
        {
            var resolveObj = _container.Resolve(resolveType);
            return resolveObj;
        }

        public static T Resolve<T>() where T : class
        {
            var resolveObj = _container.Resolve<T>();
            return resolveObj;
        }

        public static ViewModelBase ResolveViewModel(Element view)
        {
            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);

            if (viewModelType == null)
            {
                throw new Exception($"Cannot locate view model type for {viewModelType}");
            }

            var viewModel = (ViewModelBase)Resolve(viewModelType);

            return viewModel;
        }
    }
}
