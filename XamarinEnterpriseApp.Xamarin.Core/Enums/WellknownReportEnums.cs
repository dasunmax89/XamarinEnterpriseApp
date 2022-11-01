using System;
namespace XamarinEnterpriseApp.Xamarin.Core.Enums
{
    public enum WellknownReportParams
    {
        Location = 1,
        Place = 2,
        SuperCategory = 3,
        MainCategory = 4,
        Category = 5,
        ReportingMethod = 6,
        ReportOwner = 7,
        ReportState = 8,
        ActionState = 9,
        ActionOwner = 10,
        ActionType = 11,
        DynamicProperty = 12,
        Gender = 13,
        Environment = 14,
    }

    public enum WellknownReportState
    {
        Afgehandeld = 1,
        Nieuw = 2,
        Overleg = 3,
        Inbehandeling = 4,
        Ingepland = 79,
    }

    public class WellknownReportStateType
    {
        public const string ITEM_FINISHED = "ITEM_FINISHED";

        public const string ITEM_PROGRESSING = "ITEM_PROGRESSING";

        public const string ITEM_ACCEPTED = "ITEM_ACCEPTED";

        public const string ITEM_PLANNED = "ITEM_PLANNED";
    }

    public class WellknownActionStateType
    {
        public const string ACTION_FINISHED = "ACTION_FINISHED";

        public const string ACTION_PROGRESSING = "ACTION_PROGRESSING";

        public const string ACTION_ACCEPTED = "ACTION_ACCEPTED";

        public const string ACTION_PLANNED = "ACTION_PLANNED";
    }


    public class WellknownActionRight
    {
        public const string CHANGE_ACTIONTYPE = "CHANGE_ACTIONTYPE";

        public const string CHANGE_DESCRIPTION = "CHANGE_DESCRIPTION";

        public const string CHANGE_REPORTING_METHOD = "CHANGE_REPORTING_METHOD";

        public const string CHANGE_ACTION_STATUS = "CHANGE_ACTION_STATUS";

        public const string CHANGE_DUE_DATE = "CHANGE_DUE_DATE";

        public const string CHANGE_ACTION_OWNER = "CHANGE_ACTION_OWNER";

        public const string CHANGE_ARRIVAL_DATE = "CHANGE_ARRIVAL_DATE";

        public const string CHANGE_FINAL_DUE_DATE = "CHANGE_FINAL_DUE_DATE";

        public const string CHANGE_PLANNED_DATE = "CHANGE_PLANNED_DATE";
    }

    public class WellknownReportRight
    {
        public const string CHANGE_REPORT_TYPE = "CHANGE_REPORT_TYPE";

        public const string CHANGE_DESCRIPTION = "CHANGE_DESCRIPTION";

        public const string CHANGE_REPORTING_METHOD = "CHANGE_REPORTING_METHOD";

        public const string CHANGE_REPORT_STATUS = "CHANGE_REPORT_STATUS";

        public const string CHANGE_DUE_DATE = "CHANGE_DUE_DATE";

        public const string CHANGE_FINAL_DUE_DATE = "CHANGE_FINAL_DUE_DATE";

        public const string CHANGE_REPORT_OWNER = "CHANGE_REPORT_OWNER";

        public const string CHANGE_ARRIVAL_DATE = "CHANGE_ARRIVAL_DATE";

        public const string CHANGE_PLANNED_DATE = "CHANGE_PLANNED_DATE";

        public const string CHANGE_AREA = "CHANGE_AREA";

        public const string CHANGE_CATEGORY = "CHANGE_CATEGORY";

        public const string CHANGE_PROPERTIES = "CHANGE_PROPERTIES";

        public const string VIEW_REPORTER = "VIEW_REPORTER";
    }
}
