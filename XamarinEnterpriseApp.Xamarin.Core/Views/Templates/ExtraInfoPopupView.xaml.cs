using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class ExtraInfoPopupView : ContentView
    {
        public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create("Caption",
                              typeof(string),
                              typeof(ExtraInfoPopupView),
                              string.Empty,
                              BindingMode.OneWay);

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

        public static readonly BindableProperty TextProperty =
        BindableProperty.Create("Text",
                             typeof(string),
                             typeof(ExtraInfoPopupView),
                             string.Empty,
                             BindingMode.OneWay);

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

        public ExtraInfoPopupView()
        {
            InitializeComponent();
            PropertyChanged += Component_PropertyChanged;
        }

        private void Component_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Caption))
            {
                TitleLabel.Text = Caption;
            }
            else if (e.PropertyName == nameof(Text))
            {
                if (Text != null)
                {
                    TextLabel.FormattedText = GetFormattedString(Text);
                }
            }
        }

        private FormattedString GetFormattedString(string text)
        {
            FormattedString formattedString = new FormattedString();

            string formattedText = RemoveHTML(text); ;

            formattedText = FormatTelephoneNumber(formattedText);

            string[] lines = Regex.Split(formattedText, @"\r\n|\r|\n");

            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] words = Regex.Split(line, @" ");

                    foreach (var word in words)
                    {
                        if (word.IsURI() || word.IsValidPhoneNo() || word.IsValidEmail())
                        {
                            formattedString.Spans.Add(new HyperlinkSpan() { Text = word, Url = word });
                        }
                        else
                        {
                            formattedString.Spans.Add(new Span() { Text = word });
                        }

                        formattedString.Spans.Add(new Span() { Text = " " });
                    }

                    formattedString.Spans.Add(new Span() { Text = Environment.NewLine });
                }
            }

            return formattedString;
        }

        private string RemoveHTML(string text)
        {
            try
            {
                Regex r = new Regex(@"<a.*?href=(""|')(?<href>.*?)(""|').*?>(?<value>.*?)");

                var matches = r.Matches(text);

                foreach (Match match in matches)
                {
                    text = text.Replace(match.ToString(), match.Groups["href"].Value + Environment.NewLine);
                }

                text = text.Replace("</br>", Environment.NewLine);

                text = Regex.Replace(text, "<.*?>", string.Empty);

            }
            catch (Exception ex)
            {
                LogHelper.LogException("RemoveHTML", ex);
            }

            return text;
        }

        private static string FormatTelephoneNumber(string text)
        {
            // Match and format Telephone number
            const string PhoneNumberPattern = @"((\+\d{1,2})?\(?\d{1,2}\)?[\s.-]?)?\(?\d{1}\)?[\s.-]?\(?\d{1}\)?[\s.-]?\(?\d{1}\)?[\s.-]?\(?\d{1}\)?[\s.-]?\(?\d{1}\)?[\s.-]?\(?\d{1}\)?[\s.-]?\(?\d{1}\)?[\s.-]?\(?\d{1}\)?[\s.-]?\(?\d{1}\)?[.-]?";
            const string SpaceRemovalPattern = "[ ().-]+";

            Match match = Regex.Match(text, PhoneNumberPattern);
            if (match.Success)
            {
                string formattedNumber = Regex.Replace(match.ToString(), SpaceRemovalPattern, string.Empty);
                formattedNumber = Regex.Replace(text, PhoneNumberPattern, formattedNumber);
                return " " + formattedNumber + " "; // Add space to start and end of number
            }
            else
            {
                return text;
            }

        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as ViewModelBase;

            vm.IsInfoPopupVisible = false;
        }
    }
}
