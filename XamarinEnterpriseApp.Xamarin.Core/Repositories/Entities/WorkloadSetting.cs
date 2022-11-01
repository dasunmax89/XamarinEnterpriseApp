using System;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Localization;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class WorkloadSetting : BaseEntity
    {
        public string SortBySetting { get; set; }
        public string SortTypeSetting { get; set; }
        public string ReportHeader1Setting { get; set; }
        public string ReportHeader2Setting { get; set; }
        public string ReportHeader3Setting { get; set; }
        public string ActionHeader1Setting { get; set; }
        public string ActionHeader2Setting { get; set; }
        public string ActionHeader3Setting { get; set; }
        public bool ShowHandleButton { get; set; }

        public static WorkloadSetting GetDefault()
        {
            WorkloadSetting workloadSetting = new WorkloadSetting()
            {
                SortBySetting = AppSettingConstants.FIELD_FINAL_SETTLED_DATE,
                SortTypeSetting = AppSettingConstants.FIELD_ASCENDING,
                ReportHeader1Setting = AppSettingConstants.FIELD_MAIN_CATEGORY,
                ReportHeader2Setting = AppSettingConstants.FIELD_STREET,
                ReportHeader3Setting = AppSettingConstants.FIELD_STATUS,
                ActionHeader1Setting = AppSettingConstants.FIELD_ACTIONTYPE,
                ActionHeader2Setting = AppSettingConstants.FIELD_STREET,
                ActionHeader3Setting = AppSettingConstants.FIELD_STATUS,
                ShowHandleButton = false,
            };
             
            return workloadSetting;
        }
    }
}
