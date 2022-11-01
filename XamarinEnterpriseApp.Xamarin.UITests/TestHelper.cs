using System;
using XamarinEnterpriseApp.Xamarin.Core.Tests;
using Xamarin.UITest;

namespace XamarinEnterpriseApp.Xamarin.UITests
{
    public static class TestHelper
    {
        public static void Login(string username, string password, IApp app)
        {
            app.Tap(c => c.Marked("BEGINNEN"));

            TestHelper.AddInitialUrlPage(TestData.UrlName, TestData.ValidURL, app);

            app.Tap(c => c.Marked("Login_EmailEntry"));
            app.ClearText(c => c.Marked("Login_EmailEntry"));
            app.EnterText(c => c.Marked("Login_EmailEntry"), username);

            app.Tap(c => c.Marked("Login_PasswordEntry"));
            app.ClearText(c => c.Marked("Login_PasswordEntry"));
            app.EnterText(c => c.Marked("Login_PasswordEntry"), password);

            app.DismissKeyboard();

            app.Tap(c => c.Marked("Login_SubmitButton"));
        }

        public static void AddNewURl(string name, string validUrl, IApp app)
        {
            app.Tap(c => c.Marked("WIJZIG OMGEVING"));
            app.Tap(c => c.Marked("NIEUWE VERBINDING"));

            app.Tap(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.ClearText(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.EnterText(c => c.Marked("AddNewEnvironment_EnvNameEntry"), name);

            app.Tap(c => c.Marked("AddNewEnvironment_WebURLEntry"));
            app.ClearText(c => c.Marked("AddNewEnvironment_WebURLEntry"));

            app.EnterText(c => c.Marked("AddNewEnvironment_WebURLEntry"), validUrl);

            app.DismissKeyboard();
            app.Tap(c => c.Marked("OPSLAAN"));

        }

        public static void AddInvalidURl(string invalidname, string invalidUrl, IApp app)
        {
            app.Tap(c => c.Marked("WIJZIG OMGEVING"));
            app.Tap(c => c.Marked("NIEUWE VERBINDING"));

            app.Tap(c => c.Marked("AddNewEnvironment_EnvNameEntry"));

            app.ClearText(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.EnterText(c => c.Marked("AddNewEnvironment_EnvNameEntry"), invalidname);

            app.Tap(c => c.Marked("AddNewEnvironment_WebURLEntry"));
            app.ClearText(c => c.Marked("AddNewEnvironment_WebURLEntry"));
            app.ClearText(c => c.Marked("AddNewEnvironment_WebURLEntry"));
            app.EnterText(c => c.Marked("AddNewEnvironment_WebURLEntry"), invalidUrl);

            app.DismissKeyboard();
            app.Tap(c => c.Marked("OPSLAAN"));
        }

        public static void AddInitialUrlPage(string name, string validUrl, IApp app)
        {
            app.Tap(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.ClearText(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.EnterText(c => c.Marked("AddNewEnvironment_EnvNameEntry"), name);
            app.DismissKeyboard();

            //app.Tap(c => c.Marked("AddNewEnvironment_WebURLEntry"));
            //app.ClearText(c => c.Marked("AddNewEnvironment_WebURLEntry"));
            //app.EnterText(c => c.Marked("AddNewEnvironment_WebURLEntry"), validUrl);

            //app.DismissKeyboard();
            app.Tap(c => c.Marked("KLAAR"));
            app.Tap(c => c.Marked("COMPANYNAME"));
            app.Tap(c => c.Marked("KLAAR"));

        }

        public static void LanguageSelctionOptions(string name, string validUrl, IApp app)
        {
            app.Tap(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.ClearText(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.EnterText(c => c.Marked("AddNewEnvironment_EnvNameEntry"), name);

            app.Tap(c => c.Marked("AddNewEnvironment_WebURLEntry"));
            app.ClearText(c => c.Marked("AddNewEnvironment_WebURLEntry"));
            app.EnterText(c => c.Marked("AddNewEnvironment_WebURLEntry"), validUrl);

            app.DismissKeyboard();
            app.Tap(c => c.Marked("KLAAR"));
        }

    }
}