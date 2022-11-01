using XamarinEnterpriseApp.Xamarin.Core.Tests;
using XamarinEnterpriseApp.Xamarin.UITests.Constants;
using NUnit.Framework;
using System;
using Xamarin.UITest;

namespace XamarinEnterpriseApp.Xamarin.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]

    public class OnBoardingSetEnvironmentTest : TestBase
    {
        Platform platform;

        public OnBoardingSetEnvironmentTest(Platform platform)
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

            app.Tap(c => c.Marked("AddNewEnvironment_EnvNameEntry"));             app.ClearText(c => c.Marked("AddNewEnvironment_EnvNameEntry"));             app.EnterText(c => c.Marked("AddNewEnvironment_EnvNameEntry"), TestData.InvalidName);

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
    }
}
