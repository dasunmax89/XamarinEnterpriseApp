using System;
using System.Globalization;
using System.Linq;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Behaviors
{
    public class InputViewValidationBehavior : Behavior<InputView>
    {
        public FormEntryValidation Validations { get; set; }

        public InputViewValidationBehavior(FormEntryValidation validations)
        {
            Validations = validations;
        }

        protected override void OnAttachedTo(InputView bindable)
        {
            bindable.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(InputView bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            try
            {
                EntryTextChanged(sender, args);
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Exception - OnEntryTextChanged", ex);
            }
        }

        private void EntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                bool isValid = false;
                bool canParse = false;
                var currentCulture = GlobalSetting.Instance.CurrentCulture;

                if (Validations != null)
                {
                    switch (Validations.InputTextDataType)
                    {
                        case UIValidationValueTypes.INTEGER:
                        case UIValidationValueTypes.INTEGER_POS:
                            canParse = long.TryParse(args.NewTextValue, NumberStyles.Integer, currentCulture, out long intVal);
                            isValid = canParse && intVal >= 0;
                            break;
                        case UIValidationValueTypes.DOUBLE:
                        case UIValidationValueTypes.DOUBLE_POS:
                            canParse = double.TryParse(args.NewTextValue, NumberStyles.Number, currentCulture, out double dblVal);
                            isValid = canParse && dblVal >= 0;
                            break;
                        case UIValidationValueTypes.INTEGER_NEG:
                            canParse = long.TryParse(args.NewTextValue, NumberStyles.Integer, currentCulture, out long intNegVal);
                            isValid = args.NewTextValue.Equals("-") || (canParse && intNegVal < 0);
                            break;
                        case UIValidationValueTypes.DOUBLE_NEG:
                            canParse = double.TryParse(args.NewTextValue, NumberStyles.Number, currentCulture, out double dblNegVal);
                            isValid = args.NewTextValue.Equals("-") || (canParse && dblNegVal < 0);
                            break;
                        case UIValidationValueTypes.TEXT:
                            if (Validations.MaxLength > 0)
                            {
                                isValid = args.NewTextValue.Length <= Validations.MaxLength;
                            }
                            break;
                        case UIValidationValueTypes.DATE:
                        case UIValidationValueTypes.TIME:
                        case UIValidationValueTypes.TIMESTAMP:
                            isValid = true;
                            break;
                        case UIValidationValueTypes.PHONENUMBER:
                        case UIValidationValueTypes.EMAILADDRESS:
                        case UIValidationValueTypes.URL:
                            if (Validations.MaxLength > 0)
                            {
                                isValid = args.NewTextValue.Length <= Validations.MaxLength;
                            }
                            break;
                        case UIValidationValueTypes.NONE:
                            if (Validations.MaxLength > 0)
                            {
                                isValid = args.NewTextValue.Length <= Validations.MaxLength;
                            }
                            break;
                    }

                    if (isValid && Validations.MaxValue > 0)
                    {
                        double.TryParse(args.NewTextValue, NumberStyles.Any, currentCulture, out double parsedVal);

                        isValid = parsedVal <= Validations.MaxValue;
                    }
                }

               ((InputView)sender).Text = isValid ? args.NewTextValue : args.OldTextValue;
            }
        }
    }
}
