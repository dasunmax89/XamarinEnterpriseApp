using System;
namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class AppNavigationEventArgs
    {
        public object Data { get; set; }

        public AppNavigationEventArgs()
        {

        }

        public T GetData<T>() where T : new()
        {
            T data = default(T);

            if (Data != null)
            {
                data = (T)Data;
            }

            return data;
        }
    }
}
