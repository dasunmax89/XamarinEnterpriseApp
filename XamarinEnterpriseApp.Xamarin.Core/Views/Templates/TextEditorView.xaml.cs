using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Behaviors;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class TextEditorView : ContentView
    {
        public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create("Caption",
                               typeof(string),
                               typeof(TextEditorView),
                               string.Empty,
                               BindingMode.OneWay);

        public static readonly BindableProperty TextProperty =
        BindableProperty.Create("Text",
                              typeof(string),
                              typeof(TextEditorView),
                              string.Empty,
                              BindingMode.TwoWay);

        public static readonly BindableProperty KeyboardProperty =
        BindableProperty.Create("Keyboard",
                             typeof(Keyboard),
                             typeof(TextEditorView),
                             Keyboard.Default,
                             BindingMode.OneWay);

        public static readonly BindableProperty IsReadOnlyProperty =
        BindableProperty.Create("IsReadOnly",
                             typeof(bool),
                             typeof(TextEditorView),
                             false,
                             BindingMode.OneWay);

        public static readonly BindableProperty ErrorMsgProperty =
        BindableProperty.Create("ErrorMsg",
                           typeof(string),
                           typeof(TextEditorView),
                           string.Empty,
                           BindingMode.OneWay);

        public static readonly BindableProperty TextboxCompletedCommandProperty =
        BindableProperty.Create("TextboxCompletedCommand",
                       typeof(ICommand),
                       typeof(TextEditorView),
                       null,
                       BindingMode.OneWay);

        public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create("Placeholder",
                      typeof(string),
                      typeof(TextEditorView),
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

        public static readonly BindableProperty TextboxFocusedCommandProperty =
        BindableProperty.Create("TextboxFocusedCommand",
                       typeof(ICommand),
                       typeof(TextEditorView),
                       null,
                       BindingMode.OneWay);

        public ICommand TextboxFocusedCommand
        {
            get
            {
                return (ICommand)GetValue(TextboxFocusedCommandProperty);
            }
            set
            {
                SetValue(TextboxFocusedCommandProperty, value);
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

        public TextEditorView()
        {
            InitializeComponent();

            var behavior = new TextLengthValidationBehavior(new FormEntryValidation()
            {
                MaxLength = 4000,
            });

            ExtendedEditor.Behaviors.Add(behavior);

            PropertyChanged += TextEditorView_PropertyChanged;

            SetReadOnlyUI(false);
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

        private void TextEditorView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
                ExtendedEditor.Placeholder = Placeholder;
            }
            else if (e.PropertyName == nameof(ErrorMsg))
            {
                ErrorLabel.Text = ErrorMsg;
                bool isError = !string.IsNullOrEmpty(ErrorMsg);
                ErrorLabel.IsVisible = isError;

                ErrorLabel.SetValue(AutomationProperties.HelpTextProperty, ErrorMsg);

                ExtendedEditor.BorderColor = isError ? Color.FromHex("#ffe0e0") : Color.Transparent;
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

        public InputView GetInputView()
        {
            return ExtendedEditor;
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs args)
        {
            Text = args.NewTextValue;
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            var vm = BindingContext as ViewModelBase;

            ExtendedEditor control = sender as ExtendedEditor;

            EditorEventArgs args = new EditorEventArgs()
            {
                Tag = Caption,
                Text = control.Text?.Trim(),
            };

            TextboxCompletedCommand?.Execute(args);
        }

        private void Entry_Focused(object sender, EventArgs e)
        {
            OnFocused(sender, true);
        }

        private void Entry_Unfocused(object sender, EventArgs e)
        {
            OnFocused(sender, false);
        }

        private void OnFocused(object sender, bool isFocused)
        {
            var vm = BindingContext as ViewModelBase;

            ExtendedEditor control = sender as ExtendedEditor;

            EditorEventArgs args = new EditorEventArgs()
            {
                Tag = Caption,
                Text = control.Text?.Trim(),
                IsFocused = isFocused,
                ScrollElement = control.Parent,

            };

            TextboxFocusedCommand?.Execute(args);
        }
    }
}
