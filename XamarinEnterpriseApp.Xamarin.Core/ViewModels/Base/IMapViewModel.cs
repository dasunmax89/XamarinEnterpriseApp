using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Models;

namespace XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base
{
    public interface IMapViewModel
    {
        int LocationCurrentState { get; set; }

        bool IsDraggablePointer { get; set; }

        bool IsMapLayerAvailable { get; set; }

        ObservableCollection<WorkloadListItem> MapLocations { get; set; }

        Task RefreshMap();

        void UpdateMap(GeographicLocation location);
    }
}
