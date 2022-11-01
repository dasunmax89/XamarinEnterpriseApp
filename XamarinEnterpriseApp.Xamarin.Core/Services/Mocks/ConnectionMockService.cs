using System;
using Plugin.Connectivity.Abstractions;

namespace XamarinEnterpriseApp.Xamarin.Core.Services.Mocks
{
    public class ConnectionMockService : IConnectionService
    {
        public bool IsConnected => true;

        public string DeviceIP => "127.0.0.1";

        public event ConnectivityChangedEventHandler ConnectivityChanged;
    }
}
