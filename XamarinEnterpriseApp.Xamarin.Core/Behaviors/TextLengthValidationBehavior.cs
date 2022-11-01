using System;
using System.Globalization;
using System.Linq;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Behaviors
{
    public class TextLengthValidationBehavior : Behavior<InputView>
    {
        public FormEntryValidation Validations { get; set; }

        public TextLengthValidationBehavior(FormEntryValidation validations)
        {
            Validations = validations;
        }

        protected override void OnAttachedTo(InputView bindable)
        {
            AddTextChangedListener(bindable, false);

            base.OnAttachedTo(bindable);
        }

        private void AddTextChangedListener(InputView bindable, bool isRemove)
        {
            if (bindable is Entry)
            {
                var entry = bindable as Entry;

                if (isRemove)
                {
                    entry.TextChanged -= OnEntryTextChanged;
                }
                else
                {
                    entry.TextChanged += OnEntryTextChanged;
                }
            }
            else if (bindable is Editor)
            {
                var entry = bindable as Editor;

                if (isRemove)
                {
                    entry.TextChanged -= OnEntryTextChanged;
                }
                else
                {
                    entry.TextChanged += OnEntryTextChanged;
                }
            }
        }

        protected override void OnDetachingFrom(InputView bindable)
        {
            AddTextChangedListener(bindable, true);
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

                if (Validations != null)
                {
                    if (Validations.MaxLength > 0)
                    {
                        isValid = args.NewTextValue.Length <= Validations.MaxLength;
                    }
                }

                if (sender is Entry)
                {
                    var entry = sender as Entry;
                    entry.Text = isValid ? args.NewTextValue : args.OldTextValue;
                }
                else if (sender is Editor)
                {
                    var entry = sender as Editor;
                    entry.Text = isValid ? args.NewTextValue : args.OldTextValue;
                }
            }
        }
    }
}
