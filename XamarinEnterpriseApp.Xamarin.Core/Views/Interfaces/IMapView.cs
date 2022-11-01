using System;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Models;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public interface IMapView
    {
        void SetCenter(GeographicLocation positionObj);
    }
}
