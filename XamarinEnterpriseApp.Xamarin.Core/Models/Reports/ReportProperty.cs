using System;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ReportProperty : ExtendedBindableObject
    {
        [JsonProperty("Prhd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long PrhdId { get; set; }

        [JsonProperty("Prhd_pthd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long PrhdPthdId { get; set; }

        [JsonProperty("Prhd_ithd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long PrhdIthdId { get; set; }

        [JsonProperty("Prhd_number")]
        public string PrhdNumber { get; set; }

        [JsonProperty("Prhd_varchar")]
        public string PrhdVarchar { get; set; }

        [JsonProperty("Prhd_date")]
        public DateTimeOffset? PrhdDate { get; set; }

        [JsonProperty("Prhd_text")]
        public string PrhdText { get; set; }

        private long _prhdPihdId;
        [JsonProperty("Prhd_pihd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long PrhdPihdId
        {
            get { return _prhdPihdId; }
            set
            {
                _prhdPihdId = value;
                RaisePropertyChanged(() => PrhdPihdId);

                PropertiePickListItem propertiePickListItem = GetPicklistItem(value);
                PickedValue = propertiePickListItem?.Label;
            }
        }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonIgnore]
        public bool IsReadOnly { get; set; }

        private ReportItemState _reportItemState;
        [JsonIgnore]
        public ReportItemState ReportItemState
        {
            get { return _reportItemState; }
            set
            {
                _reportItemState = value;
                RaisePropertyChanged(() => ReportItemState);
                RaisePropertyChanged(() => Caption);
            }
        }

        private string _errorMsg;
        [JsonIgnore]
        public string ErrorMsg
        {
            get { return _errorMsg; }
            set
            {
                _errorMsg = value;
                RaisePropertyChanged(() => ErrorMsg);
            }
        }

        [JsonIgnore]
        public string Caption
        {
            get
            {
                string caption = string.Empty;

                bool isMandatory = CheckMandatory(ReportItemState);

                string mandatoryText = isMandatory ? "*" : "";

                caption = $"{PropertyType.Label} {mandatoryText}";

                return caption;
            }
        }

        [JsonIgnore]
        public PropertieType PropertyType { get; set; }

        private string _pickedValue;
        [JsonIgnore]
        public string PickedValue
        {
            get { return _pickedValue; }
            set
            {
                _pickedValue = value;
                RaisePropertyChanged(() => PickedValue);
            }
        }

        private DateTime _pickedDate;
        [JsonIgnore]
        public DateTime PickedDate
        {
            get { return _pickedDate; }
            set
            {
                _pickedDate = value;
                RaisePropertyChanged(() => PickedDate);
                PrhdDate = value.DateTimeOffset();
            }
        }

        public WellknownUIControlType GetControlType()
        {
            WellknownUIControlType controlType = WellknownUIControlType.NONE;

            if (PropertyType.Type == "Number")
            {
                controlType = WellknownUIControlType.NUMBER;
            }
            else if (PropertyType.Type == "Text")
            {
                controlType = WellknownUIControlType.TEXT;
            }
            else if (PropertyType.Type == "Memo")
            {
                controlType = WellknownUIControlType.MEMO;
            }
            else if (PropertyType.Type == "Varchar2")
            {
                controlType = WellknownUIControlType.VARCHAR;
            }
            else if (PropertyType.Type == "Date")
            {
                controlType = WellknownUIControlType.DATETIME;
            }
            else if (PropertyType.Type == "List")
            {
                controlType = WellknownUIControlType.PICKLIST;
            }
            else
            {
                controlType = WellknownUIControlType.VARCHAR;
            }

            return controlType;
        }

        public ReportProperty()
        {

        }

        public PropertiePickListItem GetPicklistItem(long id)
        {
            PropertiePickListItem propertiePickListItem;

            propertiePickListItem = PropertyType?.PropertiePickLists?.FirstOrDefault(x => x.Id == id);

            return propertiePickListItem;
        }

        public bool Validate(ReportItemState selectedReportStatus)
        {
            bool isValid = true;

            WellknownUIControlType controlType = GetControlType();

            bool isMandatory = CheckMandatory(selectedReportStatus);

            switch (controlType)
            {
                case WellknownUIControlType.TEXT:
                case WellknownUIControlType.MEMO:
                    if (isMandatory && string.IsNullOrEmpty(PrhdText))
                    {
                        isValid = false;
                    }
                    break;
                case WellknownUIControlType.NUMBER:
                    if (isMandatory && string.IsNullOrEmpty(PrhdNumber))
                    {
                        isValid = false;
                    }
                    break;
                case WellknownUIControlType.DATETIME:
                    if (isMandatory && PickedDate == DateTime.MinValue)
                    {
                        isValid = false;
                    }
                    PrhdDate = PickedDate.DateTimeOffset();
                    break;
                case WellknownUIControlType.VARCHAR:
                    if (isMandatory && string.IsNullOrEmpty(PrhdVarchar))
                    {
                        isValid = false;
                    }
                    break;
                case WellknownUIControlType.PICKLIST:
                    if (isMandatory && string.IsNullOrEmpty(PickedValue))
                    {
                        isValid = false;
                    }
                    break;
                case WellknownUIControlType.NONE:
                    break;
            }

            return isValid;
        }

        public bool CheckMandatory(ReportItemState selectedReportStatus)
        {
            bool isMandatory = false;

            bool indMandatory = PropertyType.IndMandatory == "Y";

            List<long> mandatoryStates = PropertyType.MandatoryStates;

            long statusId = selectedReportStatus != null ? selectedReportStatus.IsttId : 0;

            bool mandetoryStatusValidation = mandatoryStates.Contains(statusId);

            isMandatory = (indMandatory && !mandatoryStates.Any()) || (indMandatory && mandetoryStatusValidation);

            return isMandatory;
        }

    }
}
