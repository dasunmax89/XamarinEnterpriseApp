using System;
using MapKit;
using XamarinEnterpriseApp.Xamarin.Core.Controls;

namespace XamarinEnterpriseApp.Xamarin.iOS.Renderers
{
    public class ExtendedMKAnnotationView : MKAnnotationView
    {
        public Pin Pin { get; set; }

        public ExtendedMKAnnotationView(IMKAnnotation annotation, string id)
            : base(annotation, id)
        {

        }
    }
}
