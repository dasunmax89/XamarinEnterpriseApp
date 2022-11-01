using System;
using XamarinEnterpriseApp.Xamarin.Core.Enums;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Controls
{
    public class EditorEventArgs
    {
        public string Tag { get; set; }

        public string Text { get; set; }

        public bool IsFocused { get; set; }

        public Element ScrollElement { get; set; }
    }
}
