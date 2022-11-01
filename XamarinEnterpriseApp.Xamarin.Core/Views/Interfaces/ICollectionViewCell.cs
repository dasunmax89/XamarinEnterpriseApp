using System;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public interface ICollectionViewCell
    {
        BindableProperty GetSelectedCommandProperty();

        BindableProperty GetDeletedCommandProperty();
    }
}
