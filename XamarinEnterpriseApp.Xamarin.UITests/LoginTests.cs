using System.Diagnostics;
using System.Linq;
using XamarinEnterpriseApp.Xamarin.Core.Tests;
using XamarinEnterpriseApp.Xamarin.UITests;
using XamarinEnterpriseApp.Xamarin.UITests.Constants;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace XamarinEnterpriseApp.Xamarin.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]

    public class LoginTests : TestBase
    {
        Platform platform;

        public LoginTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void FirstLaunchPageTest()
        {
            AssertForElement("BEGINNEN");
        }

        [Test]
        public void LoginFormUIVerification()
        {
            app.Tap(c => c.Marked("BEGINNEN"));

            TestHelper.AddInitialUrlPage(TestData.UrlName, TestData.ValidURL, app);

            // added new launguage
            //app.Tap(c => c.Marked("Dutch"));
            //app.Tap(c => c.Marked("KLAAR"));

            AssertForElement("Login_EmailEntry");


            AssertForElement("Login_PasswordEntry");

            app.Screenshot("LoginFormUIVerification");
        }

        [Test]
        public void LoginWithValidCredentials()
        {
            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);

            // Verify if it reached Main Screen
            //  AssertForElement("Main_Grid");

            // verifying "Mijn Werkvoorraad" label is present
            AssertForElement("Mijn Werkvoorraad");
            app.Screenshot("LoginWithValidCredentials");
        }

        [Test]
        public void Logout()
        {
            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);

            bool isSettingsButtonPresent = AssertForElement("Header_Settings_Button");

            app.Tap(c => c.Marked("Header_Settings_Button"));

            bool isLogoutButtonPresent = AssertForElement("Uitloggen");

            app.Tap(c => c.Marked("Uitloggen"));

            app.Tap(c => c.Marked("Ja"));
        }

        [Test]
        public void AddURLPage()
        {
            string name = TestData.UrlName;
            string validUrl = TestData.ValidURL;

            app.Tap(c => c.Marked("BEGINNEN"));

            TestHelper.AddInitialUrlPage(name, validUrl, app);
            TestHelper.AddNewURl(name, validUrl, app);

            //AssertForElement(TestData.UrlName);
            app.Screenshot(" Naam verbinding value is  displayed in the grid");

        }

        /******************************************************************
       * This test is writen to verify the error message for invalid Urls.  
       ****************************************************************/

        [Test]
        public void AddInvalidURL()
        {
            app.Tap(c => c.Marked("BEGINNEN"));

            TestHelper.AddInitialUrlPage(TestData.UrlName, TestData.ValidURL, app);

            TestHelper.AddInvalidURl(TestData.InvalidName, TestData.InvalidURL, app);

            AssertForElement(Messages.InvalidUrlMsg);
            // bool isInvalidURLMessagePresent = AssertForElement("Voer een geldig webadres in");
            app.Screenshot("User has entered invalid URL");
        }

        /***********************************************************************
         * This test is writen to verify that user cannot enter an invalid URL. 
         * Multiple invalid URl formats will be tested. 
         * And error message will display for incorect URL.
         * 
         * MMF #115680 https://tp.com.companyname.com/TargetProcess2/entity/115680-automation-verify-user-cannot-enter-invalid
        ***********************************************************************/

        [Test]
        public void TestURLValidation()
        {
            string[] urlTest = new string[3];

            urlTest[0] = "www.google.com";
            urlTest[1] = "https:/test.com";
            urlTest[2] = "www.testme.ap/";

            app.Tap(c => c.Marked("BEGINNEN"));
            TestHelper.AddInitialUrlPage(TestData.UrlName, TestData.ValidURL, app);
            app.Tap(c => c.Marked("WIJZIG OMGEVING"));
            app.Tap(c => c.Marked("NIEUWE VERBINDING"));

            app.Tap(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.ClearText(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.EnterText(c => c.Marked("AddNewEnvironment_EnvNameEntry"), TestData.InvalidName);

            app.DismissKeyboard();

            for (int i = 0; i < urlTest.Length; i++)
            {
                app.Tap(c => c.Marked("AddNewEnvironment_WebURLEntry"));
                app.ClearText(c => c.Marked("AddNewEnvironment_WebURLEntry"));
                app.EnterText(c => c.Marked("AddNewEnvironment_WebURLEntry"), urlTest[i]);
                app.DismissKeyboard();
                app.Tap(c => c.Marked("OPSLAAN"));
                AssertForElement(Messages.InvalidUrlMsg);
                app.Screenshot("User has entered invalid URL");
            }
        }

        /************************************************************************
         * This test will verify user cannot submit emptyfield in the add URL page.
         * name and URl fields are mandotory fields. Error messages will be displayed 
         * for empty Url-name field and URL fields.
         * Error message content will be verified
         * 
         * MMF #115588 https://tp.com.companyname.com/TargetProcess2/entity/115588-automation-verify-user-cannot-submit-empty
        ***********************************************************************/

        [Test]
        public void ValidateEmptyFields()
        {
            app.Tap(c => c.Marked("BEGINNEN"));
            TestHelper.AddInitialUrlPage(TestData.UrlName, TestData.ValidURL, app);
            app.Tap(c => c.Marked("WIJZIG OMGEVING"));
            app.Tap(c => c.Marked("NIEUWE VERBINDING"));

            app.Tap(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.ClearText(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.DismissKeyboard();

            app.Tap(c => c.Marked("OPSLAAN"));

            AssertForElement(Messages.MissingValueInTheNameField);

            app.Screenshot(" No value for Naam verbinding field");

            app.Tap(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.ClearText(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.EnterText(c => c.Marked("AddNewEnvironment_EnvNameEntry"), TestData.UrlName);

            app.Tap(c => c.Marked("AddNewEnvironment_WebURLEntry"));
            app.ClearText(c => c.Marked("AddNewEnvironment_WebURLEntry"));
            app.EnterText(c => c.Marked("AddNewEnvironment_WebURLEntry"), " ");

            app.DismissKeyboard();
            app.Tap(c => c.Marked("OPSLAAN"));

            AssertForElement(Messages.MissingValueInTheUrlField);
            app.Screenshot("User has entered invalid URL");
        }

        /***********************************************************************
         * This test is written to verify the error message for invalid
         * credentials . Message content will be verified
         *        
         * MMF #115730 https://tp.com.companyname.com/TargetProcess2/entity/115730-automation-verify-the-error-message-for     
 
        ************************************************************************/

        [Test]
        public void LoginWithInValidCredentials()
        {
            string invalidusername = TestData.InvalidUserName;
            string invalidpassword = TestData.InvalidPassword;

            TestHelper.Login(invalidusername, invalidpassword, app);
            bool isPresent = CheckForElement(Messages.InvalidCredentialsMessage);
            Assert.IsTrue(isPresent);
            app.Screenshot(" Check error message for invalid credentials ");
        }

        /************************************************************************************
         * This is written to verify that user can select prefered language from the list when user boot up the app for the first time 
         * MMF ID #  11801 https://com.companyname.tpondemand.com/entity/118011-automation_as-a-user-i-want-to
         * *************************************************************************************/

        // Sprint 9
        [Test]
        public void LanguageSelectionPage()
        {
            app.Tap(c => c.Marked("BEGINNEN"));
            TestHelper.LanguageSelctionOptions(TestData.UrlName, TestData.ValidURL, app);

            app.Tap(c => c.Marked("COMPANYNAME"));
            app.Tap(c => c.Marked("For Gayathri"));
            app.Tap(c => c.Marked("Nederlands"));
            app.Tap(c => c.Marked("VORIGE"));

            /* Welcome screen validation.
             * This will validate "VORIGE" button will navigate to the welcome page */

            AssertForElement("BEGINNEN");

            app.Tap(c => c.Marked("BEGINNEN"));
            app.Tap(c => c.Marked("COMPANYNAME"));
            app.Tap(c => c.Marked("KLAAR"));

            /* Login screen validation
             * This will validate pressing 'KLAAR' (next) button will take you to the login screen */

            AssertForElement("AANMELDEN");

        }
    }
}