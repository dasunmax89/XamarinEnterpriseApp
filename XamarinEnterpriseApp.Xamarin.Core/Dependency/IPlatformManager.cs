using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Dependency
{
    public interface IPlatformManager
    {
        string GetIPAddress();

        Task<bool> CheckPushEnabled(bool invokeSettings = false);

        void OpenPushSettings();

        void IncrementBadgeCount(bool isDecrement = false);

        void MarkAsRead(long reportId, long custId);

        void MaintainBadgeCount();

        List<ListItemModel> GetNavigationActionsheetItems(GeographicLocation position);

        void OpenGoogleMaps(GeographicLocation position);

        void OpenUber(GeographicLocation position);

        void OpenWaze(GeographicLocation position);

        Task SaveFile(ReportFile file);

        string GetDeviceId();

        string ResizeImage(string source, double width, double height);

        Task<byte[]> ResizeImage(byte[] imageData, int quality, bool toRotate);

        bool IsVoiceOverOn();

        void PostNotification(string message, int type = 0);

        void SetStatusBarColor(Color color);

    }
}
