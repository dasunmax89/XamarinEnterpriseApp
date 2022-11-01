using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Behaviors;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class TextEntryView : ContentView
    {
        public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create("Caption",
                               typeof(string),
                               typeof(TextEntryView),
                               string.Empty,
                               BindingMode.OneWay);

        public static readonly BindableProperty TextProperty =
        BindableProperty.Create("Text",
                              typeof(string),
                              typeof(TextEntryView),
                              string.Empty,
                              BindingMode.TwoWay);

        public static readonly BindableProperty KeyboardProperty =
        BindableProperty.Create("Keyboard",
                             typeof(Keyboard),
                             typeof(TextEntryView),
                             Keyboard.Default,
                             BindingMode.OneWay);

        public static readonly BindableProperty IsReadOnlyProperty =
        BindableProperty.Create("IsReadOnly",
                             typeof(bool),
                             typeof(TextEntryView),
                             false,
                             BindingMode.OneWay);

        public static readonly BindableProperty ErrorMsgProperty =
        BindableProperty.Create("ErrorMsg",
                           typeof(string),
                           typeof(TextEntryView),
                           string.Empty,
                           BindingMode.OneWay);

        public static readonly BindableProperty TextboxCompletedCommandProperty =
        BindableProperty.Create("TextboxCompletedCommand",
                       typeof(ICommand),
                       typeof(TextEntryView),
                       null,
                       BindingMode.OneWay);

        public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create("Placeholder",
                              typeof(string),
                              typeof(TextEntryView),
                              null,
                              BindingMode.OneWay);


        public ICommand TextboxCompletedCommand
        {
            get
            {
                return (ICommand)GetValue(TextboxCompletedCommandProperty);
            }
            set
            {
                SetValue(TextboxCompletedCommandProperty, value);
            }
        }

        public static readonly BindableProperty TextboxChangedCommandProperty =
        BindableProperty.Create("TextboxChangedCommand",
                       typeof(ICommand),
                       typeof(TextEntryView),
                       null,
                       BindingMode.OneWay);

        public ICommand TextboxChangedCommand
        {
            get
            {
                return (ICommand)GetValue(TextboxChangedCommandProperty);
            }
            set
            {
                SetValue(TextboxChangedCommandProperty, value);
            }
        }

        public string ErrorMsg
        {
            get
            {
                return (string)GetValue(ErrorMsgProperty);
            }
            set
            {
                SetValue(ErrorMsgProperty, value);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return (bool)GetValue(IsReadOnlyProperty);
            }
            set
            {
                SetValue(IsReadOnlyProperty, value);
            }
        }

        public string Caption
        {
            get
            {
                return (string)GetValue(CaptionProperty);
            }
            set
            {
                SetValue(CaptionProperty, value);
            }
        }

        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }
            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }

        public Keyboard Keyboard
        {
            get
            {
                return (Keyboard)GetValue(KeyboardProperty);
            }
            set
            {
                SetValue(KeyboardProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public TextEntryView()
        {
            InitializeComponent();

            var behavior = new TextLengthValidationBehavior(new FormEntryValidation()
            {
                MaxLength = 100,
            });

            ExtendedEditor.Behaviors.Add(behavior);

            PropertyChanged += TextEntryView_PropertyChanged;

            SetReadOnlyUI(false);

            ErrorLabel.Focused += ErrorLabel_Focused;
        }

        private void ErrorLabel_Focused(object sender, FocusEventArgs e)
        {

        }

        private void SetReadOnlyUI(bool isReadOnly)
        {
            ExtendedEditor.IsEnabled = !isReadOnly;

            if (isReadOnly)
            {
                var color = Color.FromHex("#f7f7f7");
                ExtendedEditor.BackgroundColor = color;
            }
            else
            {
                var color = Color.FromHex("#F1F3F5");
                ExtendedEditor.BackgroundColor = color;
            }
        }

        public void AddBehavior(InputViewValidationBehavior behavior)
        {
            ExtendedEditor.Behaviors.Add(behavior);
        }

        private void TextEntryView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Caption))
            {
                CaptionLabel.Text = Caption;

                string captionHelpText = Caption?.Replace("*", "." + AppResources.Mandatory);

                CaptionLabel.SetValue(AutomationProperties.HelpTextProperty, captionHelpText);

                SetValue(AutomationProperties.HelpTextProperty, string.Format(AppResources.EnterValueFor, captionHelpText));
            }
            else if (e.PropertyName == nameof(Text))
            {
                ExtendedEditor.Text = Text;
            }
            else if (e.PropertyName == nameof(Keyboard))
            {
                ExtendedEditor.Keyboard = Keyboard != null ? Keyboard : Keyboard.Default;
            }
            else if (e.PropertyName == nameof(IsReadOnly))
            {
                SetReadOnlyUI(IsReadOnly);
            }
            else if (e.PropertyName == nameof(Placeholder))
            {
                //ExtendedEditor.Placeholder = Placeholder;
            }
            else if (e.PropertyName == nameof(ErrorMsg))
            {
                ErrorLabel.Text = ErrorMsg;
                bool isError = !string.IsNullOrEmpty(ErrorMsg);
                ErrorLabel.IsVisible = isError;

                ExtendedEditor.BorderColor = isError ? Color.FromHex("#ffe0e0") : Color.Transparent;

                ErrorLabel.SetValue(AutomationProperties.HelpTextProperty, ErrorMsg);

                if (isError)
                {
                    ErrorLabel.Focus();
                }
                else
                {
                    ErrorLabel.Unfocus();
                }
            }
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs args)
        {
            Text = args.NewTextValue;

            EditorEventArgs editorEventArgs = new EditorEventArgs()
            {
                Tag = Caption,
                Text = Text?.Trim(),
            };

            TextboxChangedCommand?.Execute(editorEventArgs);
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            var vm = BindingContext as ViewModelBase;

            RoundedEntry control = sender as RoundedEntry;

            EditorEventArgs editorEventArgs = new EditorEventArgs()
            {
                Tag = Caption,
                Text = control.Text?.Trim(),
            };

            TextboxCompletedCommand?.Execute(editorEventArgs);
        }

        public InputView GetInputView()
        {
            return ExtendedEditor;
        }
    }
}
