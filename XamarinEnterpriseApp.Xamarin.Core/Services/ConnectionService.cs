using System;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Services
{
    public interface IConnectionService
    {
        bool IsConnected { get; }
        string DeviceIP { get; }
        event ConnectivityChangedEventHandler ConnectivityChanged;
    }

    public class ConnectionService : BaseService, IConnectionService
    {
        public string DeviceIP
        {
            get
            {
                string ipAddress = DependencyService.Get<IPlatformManager>().GetIPAddress();
                return ipAddress;
            }
        }

        private readonly IConnectivity _connectivity;

        public ConnectionService()
        {
            _connectivity = CrossConnectivity.Current;
            _connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            ConnectivityChanged?.Invoke(this, new ConnectivityChangedEventArgs() { IsConnected = e.IsConnected });
        }

        public bool IsConnected => _connectivity.IsConnected;

        public event ConnectivityChangedEventHandler ConnectivityChanged;
    }
}
