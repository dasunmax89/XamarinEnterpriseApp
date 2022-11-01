using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public interface IFooterView
    {
        ICommand LeftCommand { get; set; }

        ICommand RightCommand { get; set; }

        Color AppFooterColor { get; }

        string LeftCommandText { get; }

        string RightCommandText { get; }

        string RightCommandHelpText { get; }

        bool IsProgressVisible { get; }

        string ProgressText { get; set; }

        bool RightFooterButtonEnabled { get; set; }

        bool LeftFooterButtonEnabled { get; set; }

        bool RightFooterButtonVisible { get; set; }

        bool LeftFooterButtonVisible { get; set; }
    }
}
