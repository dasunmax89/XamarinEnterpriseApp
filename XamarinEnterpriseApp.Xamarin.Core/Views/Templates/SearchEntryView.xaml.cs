using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Behaviors;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class SearchEntryView : ContentView
    {
        public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create("Caption",
                               typeof(string),
                               typeof(SearchEntryView),
                               string.Empty,
                               BindingMode.OneWay);

        public static readonly BindableProperty KeyboardProperty =
        BindableProperty.Create("Keyboard",
                             typeof(Keyboard),
                             typeof(SearchEntryView),
                             Keyboard.Default,
                             BindingMode.OneWay);

        public static readonly BindableProperty IsReadOnlyProperty =
        BindableProperty.Create("IsReadOnly",
                             typeof(bool),
                             typeof(SearchEntryView),
                             false,
                             BindingMode.OneWay);


        public static readonly BindableProperty TextboxCompletedCommandProperty =
        BindableProperty.Create("TextboxCompletedCommand",
                         typeof(ICommand),
                         typeof(SearchEntryView),
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
                       typeof(SearchEntryView),
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

        public SearchEntryView()
        {
            InitializeComponent();

            var behavior = new TextLengthValidationBehavior(new FormEntryValidation()
            {
                MaxLength = 25,
            });

            AddBehavior(behavior);

            PropertyChanged += SearchEntryView_PropertyChanged;

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
                var color = Color.FromHex("#f1f3f5");
                ExtendedEditor.BackgroundColor = color;
            }
        }

        public void AddBehavior(Behavior behavior)
        {
            ExtendedEditor.Behaviors.Add(behavior);
        }

        private void SearchEntryView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Caption))
            {
                ExtendedEditor.Placeholder = Caption;
            }
            else if (e.PropertyName == nameof(Keyboard))
            {
                ExtendedEditor.Keyboard = Keyboard != null ? Keyboard : Keyboard.Default;
            }
            else if (e.PropertyName == nameof(IsReadOnly))
            {
                SetReadOnlyUI(IsReadOnly);
            }
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs args)
        {
            EditorEventArgs editorEventArgs = new EditorEventArgs()
            {
                Tag = "HeaderSearch",
                Text = args.NewTextValue,
            };

            TextboxChangedCommand?.Execute(editorEventArgs);
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            RoundedEntry control = sender as RoundedEntry;

            EditorEventArgs args = new EditorEventArgs()
            {
                Tag = Caption,
                Text = control.Text?.Trim(),
            };

            TextboxCompletedCommand?.Execute(args);
        }
    }
}
