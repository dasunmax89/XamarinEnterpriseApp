using System;
using System.Collections.Generic;
using System.Linq;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class RelationAttribute : ExtendedBindableObject
    {
        [JsonProperty("JSON_field_name")]
        public string JsonFieldName { get; set; }

        [JsonProperty("Field_name")]
        public string FieldName { get; set; }

        [JsonProperty("Field_label")]
        public string FieldLabel { get; set; }

        [JsonProperty("Field_visible")]
        public bool FieldVisible { get; set; }

        [JsonProperty("Field_length")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long FieldLength { get; set; }

        [JsonProperty("listInstances")]
        public List<RelationAttributeListItem> PropertiePickLists { get; set; }

        [JsonProperty("Field_type")]
        public string FieldType { get; set; }

        [JsonProperty("ValidationError")]
        public string ValidationError { get; set; }

        [JsonProperty("Mandatory")]
        public bool IsMandatory { get; set; }

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
        public string Value { get; set; }

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

        [JsonIgnore]
        public string Caption
        {
            get
            {
                bool isMandatory = IsMandatory;

                string mandatoryText = isMandatory ? "*" : "";

                string caption = $"{FieldLabel} {mandatoryText}";

                return caption;
            }
        }

        [JsonIgnore]
        public int Index { get; private set; }

        public RelationAttributeListItem GetPicklistItem(string label)
        {
            RelationAttributeListItem propertiePickListItem;

            propertiePickListItem = PropertiePickLists?.FirstOrDefault(x => x.Label == label);

            return propertiePickListItem;
        }

        public bool Validate()
        {
            bool isValid = true;

            bool isMandatory = IsMandatory;

            string evaluateVal = FieldType == "LIST" ? PickedValue : Value;

            string fieldLabel = FieldLabel?.ToLower();

            if (isMandatory && string.IsNullOrEmpty(evaluateVal))
            {
                isValid = false;

                ErrorMsg = !string.IsNullOrEmpty(ValidationError) ? ValidationError : string.Format(AppResources.EnterValidField, fieldLabel);

                return isValid;
            }

            switch (FieldType)
            {
                case "STRING":

                    break;
                case "NUMBER":
                    bool canParse = long.TryParse(evaluateVal, out long number);
                    if (evaluateVal != null && !canParse)
                    {
                        ErrorMsg = !string.IsNullOrEmpty(ValidationError) ? ValidationError : string.Format(AppResources.EnterValidField, fieldLabel);

                        isValid = false;
                    }
                    break;
                case "ZIPCODE":

                    break;
                case "TELEPHONE":
                    if (evaluateVal != null && !evaluateVal.IsValidPhoneNo())
                    {
                        ErrorMsg = !string.IsNullOrEmpty(ValidationError) ? ValidationError : string.Format(AppResources.EnterValidField, fieldLabel);

                        isValid = false;
                    }
                    break;
                case "EMAIL":
                    if (evaluateVal != null && !evaluateVal.IsValidEmail())
                    {
                        ErrorMsg = !string.IsNullOrEmpty(ValidationError) ? ValidationError : string.Format(AppResources.EnterValidField, fieldLabel);

                        isValid = false;
                    }
                    break;
                case "LIST":

                    break;
                default:

                    break;
            }

            if (isValid)
            {
                ErrorMsg = string.Empty;
            }

            return isValid;
        }

        internal static RelationAttribute FromIndex(int v)
        {
            return new RelationAttribute()
            {
                Index = v,
            };
        }

        public static List<RelationAttributeListItem> GetGenderPickItems()
        {
            return new List<RelationAttributeListItem>() {
                                new RelationAttributeListItem() {
                                    Label=AppResources.Male,
                                    Value= "M"
                                },
                                new RelationAttributeListItem()  {
                                    Label=AppResources.Female,
                                    Value= "V"
                                },
                                new RelationAttributeListItem()  {
                                    Label= AppResources.Unknown,
                                        Value= "U"
                                } };
        }
    }
}