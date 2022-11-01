
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;
using GeoCoordinatePortable;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class WorkloadListItem : MarkerListItem
    {
        public string Header1 { get; set; }

        public string Header2 { get; set; }

        public string Header3 { get; set; }

        public string SortingField { get; set; }

        public string SortingValue { get; set; }

        public ReportItem ReportItem { get; internal set; }

        public DateTimeOffset? Date { get; set; }

        public GeographicLocation Position { get; set; }

        public string LocationDesc { get; set; }

        public int Index { get; set; }

        public List<WorkloadListItem> Siblings { get; set; }

        public List<WorkloadListItem> ParentList { get; set; }

        public double Distance { get; set; }

        public long IthdId { get; set; }

        public long CustId { get; set; }

        public bool IsClustered { get; set; }

        public string SortColor { get; set; }

        public bool IsFromPush { get; set; }

        public bool IsDirectFromHome { get; set; }

        ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                RaisePropertyChanged(() => ImageSource);
            }
        }

        private bool _isFollowed;
        public bool IsFollowed
        {
            get
            {
                return _isFollowed;
            }

            set
            {
                _isFollowed = value;
                RaisePropertyChanged(() => IsFollowed);
            }
        }

        public override string MarkerText
        {
            get
            {
                return Siblings != null && Siblings.Any() ? (Siblings.Count + 1).ToString() : string.Empty;
            }
        }

        public string TitleText
        {
            get
            {
                return $"#{IthdId}";
            }
        }

        public ObservableCollection<ListItemModel> InfoList
        {
            get
            {
                var resources = Application.Current.Resources;

                var headerStyle = resources["TableItemCaptionStyle"] as Style;
                var detailStyle = resources["TableItemValueStyle"] as Style;

                List<ListItemModel> userData = new List<ListItemModel>()
                {
                    new ListItemModel()
                    {
                        Caption = AppResources.Location,
                        Header = $"{ReportItem.Location}",
                        HeaderStyle = headerStyle,
                        DetailStyle = detailStyle,
                    },

                };
                return userData.ToObservableCollection();
            }
        }

        public double PositionFactor
        {
            get
            {
                double positionFactor = -1;

                if (Position != null)
                {
                    positionFactor = Position.Latitude * Position.Longitude;
                }

                return positionFactor;

            }
        }

        public WorkloadListItem()
        {
            LocationDesc = string.Empty;
            Siblings = new List<WorkloadListItem>();
        }

        public static string GetReportHeader(Report dataItem, string headerSetting, bool isMainHeader = false)
        {
            string header = string.Empty;

            switch (headerSetting)
            {
                case AppSettingConstants.FIELD_MAIN_CATEGORY:
                    header = dataItem.MainCategory;
                    break;
                case AppSettingConstants.FIELD_SUB_CATEGORY:
                    header = dataItem.Category;
                    break;
                case AppSettingConstants.FIELD_WIJIK:
                    header = dataItem.MainArea;
                    break;
                case AppSettingConstants.FIELD_CASE_NUMBER:
                    header = dataItem.CaseNumber;
                    break;
                case AppSettingConstants.FIELD_BUURT:
                    header = string.Empty;
                    break;
                case AppSettingConstants.FIELD_REPORT_ID:
                    header = dataItem.IthdId.ToString();
                    break;
                case AppSettingConstants.FIELD_STATUS:
                    header = dataItem.Status;
                    break;
                case AppSettingConstants.FIELD_ARRIVAL_DATE:
                    header = dataItem.DateEntry.FormatDate();
                    break;
                case AppSettingConstants.FIELD_SETTLED_DATE:
                    header = dataItem.DateFinished.FormatDate();
                    break;
                case AppSettingConstants.FIELD_FINAL_SETTLED_DATE:
                    header = dataItem.DateFinishedTarget.FormatDate();
                    break;
                case AppSettingConstants.FIELD_SCHEDULED_DATE:
                    header = dataItem.DatePlanned.FormatDate();
                    break;
                case AppSettingConstants.FIELD_PLACE:
                    header = dataItem.LocationTown;
                    break;
                case AppSettingConstants.FIELD_STREET:
                    header = dataItem.LocationDesc;
                    break;
            }

            if (isMainHeader)
            {
                header = $"#{dataItem.IthdId} {header}";
            }

            return header;
        }

        public bool StatusCheck(string state)
        {
            bool contains = false;

            bool isHeaderContains = ReportItem.Status == state;

            if (isHeaderContains)
            {
                contains = isHeaderContains;
            }
            else
            {
                contains = Siblings.Any(x => x.StatusCheck(state));
            }

            return contains;
        }

        public static string GetActionHeader(ReportAction dataItem, string headerSetting, bool isMainHeader = false)
        {
            string header = string.Empty;

            switch (headerSetting)
            {
                case AppSettingConstants.FIELD_REPORT_ID:
                    header = dataItem.IthdId.ToString();
                    break;
                case AppSettingConstants.FIELD_ACTION_NUMBER:
                    header = dataItem.AchdId.ToString();
                    break;
                case AppSettingConstants.FIELD_ACTIONTYPE:
                    header = dataItem.ActionType;
                    break;
                case AppSettingConstants.FIELD_STATUS:
                    header = dataItem.Status;
                    break;
                case AppSettingConstants.FIELD_CASE_NUMBER:
                    header = dataItem.CaseNumber;
                    break;
                case AppSettingConstants.FIELD_ARRIVAL_DATE:
                    header = dataItem.DateEntry.FormatDate();
                    break;
                case AppSettingConstants.FIELD_SETTLED_DATE:
                    header = dataItem.DateFinished.FormatDate();
                    break;
                case AppSettingConstants.FIELD_FINAL_SETTLED_DATE:
                    header = dataItem.DateFinishedTarget.FormatDate();
                    break;
                case AppSettingConstants.FIELD_SCHEDULED_DATE:
                    header = dataItem.DatePlanned.FormatDate();
                    break;
                case AppSettingConstants.FIELD_PLACE:
                    header = dataItem.LocationTown;
                    break;
                case AppSettingConstants.FIELD_STREET:
                    header = dataItem.LocationDesc;
                    break;
            }

            if (isMainHeader)
            {
                header = $"#{dataItem.AchdId} {header}";
            }

            return header;
        }

        public override void SetMarkerIcon(bool isSelected)
        {

        }

        public static void SetActionSortFields(WorkloadListItem workloadListItem, ReportAction dataItem, string sortBySetting)
        {
            switch (sortBySetting)
            {
                case AppSettingConstants.FIELD_REPORT_ID:
                    workloadListItem.SortingField = dataItem.IthdId.ToString();
                    workloadListItem.SortingValue = dataItem.IthdId.ToString();
                    break;
                case AppSettingConstants.FIELD_STATUS:
                    workloadListItem.SortingField = dataItem.Status;
                    workloadListItem.SortingValue = dataItem.Status;
                    break;
                case AppSettingConstants.FIELD_CASE_NUMBER:
                    workloadListItem.SortingField = dataItem.CaseNumber;
                    workloadListItem.SortingValue = dataItem.CaseNumber;
                    break;
                case AppSettingConstants.FIELD_ACTIONTYPE:
                    workloadListItem.SortingField = dataItem.ActionType;
                    workloadListItem.SortingValue = dataItem.ActionType;
                    break;
                case AppSettingConstants.FIELD_ARRIVAL_DATE:
                    workloadListItem.SortingField = dataItem.DateEntry.FormatDate();
                    workloadListItem.SortingValue = dataItem.DateEntry.ToUnixTimeMilliseconds().ToString();
                    break;
                case AppSettingConstants.FIELD_SETTLED_DATE:
                    workloadListItem.SortingField = dataItem.DateFinished.FormatDate();
                    workloadListItem.SortingValue = dataItem.DateFinished.ToUnixTimeMilliseconds().ToString();
                    break;
                case AppSettingConstants.FIELD_FINAL_SETTLED_DATE:
                    workloadListItem.SortingField = dataItem.DateFinishedTarget.FormatDate();
                    workloadListItem.SortingValue = dataItem.DateFinishedTarget.ToUnixTimeMilliseconds().ToString();
                    break;
                case AppSettingConstants.FIELD_SCHEDULED_DATE:
                    workloadListItem.SortingField = dataItem.DatePlanned.FormatDate();
                    workloadListItem.SortingValue = dataItem.DatePlanned.ToUnixTimeMilliseconds().ToString();
                    break;
                case AppSettingConstants.FIELD_PLACE:
                    workloadListItem.SortingField = dataItem.LocationTown;
                    workloadListItem.SortingValue = dataItem.LocationTown;
                    break;
                case AppSettingConstants.FIELD_STREET:
                    workloadListItem.SortingField = dataItem.LocationDesc;
                    workloadListItem.SortingValue = dataItem.LocationDesc;
                    break;
            }
        }

        public static void SetReportSortFields(WorkloadListItem workloadListItem, Report dataItem, string sortBySetting)
        {
            switch (sortBySetting)
            {
                case AppSettingConstants.FIELD_REPORT_ID:
                    workloadListItem.SortingField = dataItem.IthdId.ToString();
                    workloadListItem.SortingValue = dataItem.IthdId.ToString();
                    break;
                case AppSettingConstants.FIELD_STATUS:
                    workloadListItem.SortingField = dataItem.Status;
                    workloadListItem.SortingValue = dataItem.Status;
                    break;
                case AppSettingConstants.FIELD_CASE_NUMBER:
                    workloadListItem.SortingField = dataItem.CaseNumber;
                    workloadListItem.SortingValue = dataItem.CaseNumber;
                    break;
                case AppSettingConstants.FIELD_MAIN_CATEGORY:
                    workloadListItem.SortingField = dataItem.MainCategory;
                    workloadListItem.SortingValue = dataItem.MainCategory;
                    break;
                case AppSettingConstants.FIELD_SUB_CATEGORY:
                    workloadListItem.SortingField = dataItem.Category;
                    workloadListItem.SortingValue = dataItem.Category;
                    break;
                case AppSettingConstants.FIELD_WIJIK:
                    workloadListItem.SortingField = dataItem.MainArea;
                    workloadListItem.SortingValue = dataItem.MainArea;
                    break;
                case AppSettingConstants.FIELD_BUURT:
                    workloadListItem.SortingField = string.Empty;
                    workloadListItem.SortingValue = string.Empty;
                    break;
                case AppSettingConstants.FIELD_ARRIVAL_DATE:
                    workloadListItem.SortingField = dataItem.DateEntry.FormatDate();
                    workloadListItem.SortingValue = dataItem.DateEntry.ToUnixTimeMilliseconds().ToString();
                    break;
                case AppSettingConstants.FIELD_SETTLED_DATE:
                    workloadListItem.SortingField = dataItem.DateFinished.FormatDate();
                    workloadListItem.SortingValue = dataItem.DateFinished.ToUnixTimeMilliseconds().ToString();
                    break;
                case AppSettingConstants.FIELD_FINAL_SETTLED_DATE:
                    workloadListItem.SortingField = dataItem.DateFinishedTarget.FormatDate();
                    workloadListItem.SortingValue = dataItem.DateFinishedTarget.ToUnixTimeMilliseconds().ToString();
                    break;
                case AppSettingConstants.FIELD_SCHEDULED_DATE:
                    workloadListItem.SortingField = dataItem.DatePlanned.FormatDate();
                    workloadListItem.SortingValue = dataItem.DatePlanned.ToUnixTimeMilliseconds().ToString();
                    break;
                case AppSettingConstants.FIELD_PLACE:
                    workloadListItem.SortingField = dataItem.LocationTown;
                    workloadListItem.SortingValue = dataItem.LocationTown;
                    break;
                case AppSettingConstants.FIELD_STREET:
                    workloadListItem.SortingField = dataItem.LocationDesc;
                    workloadListItem.SortingValue = dataItem.LocationDesc;
                    break;

            }
        }

        internal static List<WorkloadListItem> Sort(List<WorkloadListItem> workloadList, WorkloadSetting workloadSetting)
        {
            switch (workloadSetting.SortBySetting)
            {
                case AppSettingConstants.FIELD_REPORT_ID:
                case AppSettingConstants.FIELD_ARRIVAL_DATE:
                case AppSettingConstants.FIELD_FINAL_SETTLED_DATE:
                case AppSettingConstants.FIELD_SCHEDULED_DATE:
                case AppSettingConstants.FIELD_SETTLED_DATE:

                    if (workloadSetting.SortTypeSetting == AppSettingConstants.FIELD_ASCENDING)
                    {
                        workloadList = workloadList.OrderBy(x => x.SortingValue.ToNumber()).ToList();
                    }
                    else if (workloadSetting.SortTypeSetting == AppSettingConstants.FIELD_DESCENDING)
                    {
                        workloadList = workloadList.OrderByDescending(x => x.SortingValue.ToNumber()).ToList();
                    }

                    break;
                case AppSettingConstants.FIELD_STATUS:
                case AppSettingConstants.FIELD_CASE_NUMBER:
                case AppSettingConstants.FIELD_MAIN_CATEGORY:
                case AppSettingConstants.FIELD_SUB_CATEGORY:
                case AppSettingConstants.FIELD_WIJIK:
                case AppSettingConstants.FIELD_STREET:
                case AppSettingConstants.FIELD_PLACE:
                case AppSettingConstants.FIELD_BUURT:

                    if (workloadSetting.SortTypeSetting == AppSettingConstants.FIELD_ASCENDING)
                    {
                        workloadList = workloadList.OrderBy(x => x.SortingValue).ToList();
                    }
                    else if (workloadSetting.SortTypeSetting == AppSettingConstants.FIELD_DESCENDING)
                    {
                        workloadList = workloadList.OrderByDescending(x => x.SortingValue).ToList();
                    }

                    break;
            }

            return workloadList;
        }

        public double DistanceTo(WorkloadListItem from, WorkloadListItem to)
        {
            double distance = AppSettingConstants.FIELD_GROUPING_THRESHOLD * 100;

            if (from.Position != null && to.Position != null)
            {
                GeoCoordinate fromGeoCoordinate = new GeoCoordinate(from.Position.Latitude, from.Position.Longitude);
                GeoCoordinate toGeoCoordinate = new GeoCoordinate(to.Position.Latitude, to.Position.Longitude);

                distance = fromGeoCoordinate.GetDistanceTo(toGeoCoordinate);

                to.Distance = distance;
            }
            else
            {

            }

            return distance;
        }

        public bool Search(string searchText)
        {
            bool isMeContains = IthdId.ToString().Search(searchText) ||
                                Header2.Search(searchText) ||
                                Header3.Search(searchText) ||
                                ReportItem != null && ReportItem.Location.Search(searchText) ||
                                ReportItem != null && ReportItem.Category.Search(searchText);

            return isMeContains;
        }
    }
}
