using XamarinEnterpriseApp.Xamarin.Core.Tests;
using XamarinEnterpriseApp.Xamarin.UITests.Constants;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace XamarinEnterpriseApp.Xamarin.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]

    public class SettingPageTest : TestBase
    {
        Platform platform;

        public SettingPageTest(Platform platform)

        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        public void FirstLaunchPageTest()
        {
            AssertForElement("BEGINNEN");
        }

        [Test]
        public void Logout()
        {
            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);

            app.WaitForElement(c => c.Marked("Header_Settings_Button"));

            bool isSettingsButtonPresent = AssertForElement("Header_Settings_Button");

            app.Tap(c => c.Marked("Header_Settings_Button"));

            bool isLogoutButtonPresent = AssertForElement("Uitloggen");

            app.Tap(c => c.Marked("Uitloggen"));

            app.Tap(c => c.Marked("Ja"));
        }

        /* This test is written to validate application settings( Applicatie)screen according to the design
         *  MMF ID # 117045
         * https://com.companyname.tpondemand.com/entity/117045-automation-instellingen-ui-verification
         */

        [Test]
        public void SettingsScreenVerification()

        {
            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);

            app.WaitForElement(c => c.Marked("Header_Settings_Button"));

            bool isSettingsButtonPresent = AssertForElement("Header_Settings_Button");

            app.Tap(c => c.Marked("Header_Settings_Button"));

            bool isApplicatieButtonPresent = AssertForElement("Applicatie");

            bool isWerkvoorraadOptionPresent = AssertForElement("Werkvoorraad");

            bool isOverButtonPresent = AssertForElement("Over");

            bool isLogoutButtonPresent = AssertForElement("Uitloggen");

            //bool isLogoutButtonPresent = AssertForElement("Uitloggen");

            app.Tap(c => c.Marked("Over"));

            app.Tap(c => c.Marked("Header_Back_Button"));

            app.Tap(c => c.Marked("Header_Back_Button"));

        }

        /*  This test is written to validate UI of settings screen according to the design
         *  MMF ID # 117045 */

        [Test]
        public void ApplicatieScreenVerification()
        {
            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);

            app.WaitForElement(c => c.Marked("Header_Settings_Button"));

            // bool isSettingsButtonPresent = AssertForElement("Header_Settings_Button");

            app.Tap(c => c.Marked("Header_Settings_Button"));
            app.Tap(c => c.Marked("Applicatie"));
            //app.Repl();

            bool isTaalPresent = AssertForElement("Taal");
            bool isVerbindingenPresent = AssertForElement("Verbindingen");
            bool isStandaardverbindingPresent = AssertForElement("Standaardverbinding");
            // bool isValidURLPresent = AssertForElement("Taal");
            bool isNIEUWE_VERBINDINGPresent = AssertForElement("NIEUWE VERBINDING");

            app.Screenshot("SettingButtonVerification");

            //app.Tap(c => c.Marked("Taal"));

            // app.Query(e => e.Id("NIEUWE VERBINDING").Invoke("getCurrentTextColor").Value<int>());
            //var color = app.Query(x => x.Marked("NIEUWE VERBINDING").Invoke("getCurrentTextColor").Value<int>());
            // var color = app.Query(c => c.Marked("NIEUWE VERBINDING").Invoke("backgroundColor").Invoke("styleString"))[0];

            //   var color = app.Query(x => x.Marked("NIEUWE VERBINDING").Invoke("getBackground").Invoke("getColor"));


            //Header_Back_Button
        }

        [Test]
        public void EditURL()
        {
            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);
            app.WaitForElement(c => c.Marked("Header_Settings_Button"));

            app.Tap(c => c.Marked("Header_Settings_Button"));
            app.Tap(c => c.Marked("Applicatie"));

            // app.Repl();
            app.WaitForElement(c => c.Marked("Header_Settings_Button"));

            app.Tap(c => c.Marked("NoResourceEntry-290"));
            app.Tap(c => c.Marked("Header_Delete_Button"));


            AssertForElement(Messages.UnabletoDeleteActiveURLMessage);
            // bool isPresent = CheckForElement(Messages.UnabletoDeleteActiveURLMessage);
            //
            //bool isPresent = CheckForElement(Messages.InvalidCredentialsMessage);
            //  Assert.IsTrue(isPresent);
            // verify below message is displayed when user try to delete  Active URL

            // text: "Actieve verbinding kan niet worden bewerkt"

            app.Tap(c => c.Marked("OK"));
            app.Tap(c => c.Marked("ANNULEREN"));

            // adding a new URl and deleteing it

            app.Tap(c => c.Marked("NIEUWE VERBINDING"));


            app.Tap(c => c.Marked("Naam verbinding"));
            app.ClearText(c => c.Marked("Naam verbinding"));
            app.EnterText(c => c.Marked("Naam verbinding"), TestData.UrlNewName1);


            app.Tap(c => c.Marked("Webadres (URL)"));
            app.ClearText(c => c.Marked("Webadres (URL)"));
            app.EnterText(c => c.Marked("Webadres (URL)"), TestData.ValidNewURL1);

            app.DismissKeyboard();
            app.Tap(c => c.Marked("OPSLAAN"));
        }
        /* This test is to verify user should be able to add multiple new envromments from setting page
         * Annuleren button  behavior (cancel the enter text) / opslaan button behavior (Save the URl )
         * MMF # https://com.companyname.tpondemand.com/entity/118012-automation_-add-new-environment-from-the */


        // Sprint 9
        [Test]

        public void AddNewURLfromSettingsPage()

        {
            List<string> urlList = new List<string>();
            urlList.Add("https://lnet.com.companyname.nl/v2.mdesk.api/api1/");
            urlList.Add("hhttps://lnet.com.companyname.nl/v2.mdesk.api/api2/");
            urlList.Add("hhttps://lnet.com.companyname.nl/v2.mdesk.api/api3/");

            List<string> urlNameList = new List<string>();
            urlNameList.Add("valid name 1");
            urlNameList.Add("valid name 2");
            urlNameList.Add("valid name 3");

            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);
            app.WaitForElement(c => c.Marked("Header_Settings_Button"));

            app.Tap(c => c.Marked("Header_Settings_Button"));
            app.Tap(c => c.Marked("Applicatie"));
            app.WaitForElement(c => c.Marked("NIEUWE VERBINDING"));

            for (int i = 0; i < urlList.Count; i++)
            {
                app.Tap(c => c.Marked("NIEUWE VERBINDING"));

                app.Tap(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
                app.ClearText(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
                app.EnterText(c => c.Marked("AddNewEnvironment_EnvNameEntry"), urlNameList[i]);
                app.DismissKeyboard();

                app.Tap(c => c.Marked("AddNewEnvironment_WebURLEntry"));
                app.ClearText(c => c.Marked("AddNewEnvironment_WebURLEntry"));
                app.EnterText(c => c.Marked("AddNewEnvironment_WebURLEntry"), urlList[i]);
                app.DismissKeyboard();
                app.Tap(c => c.Marked("OPSLAAN"));
            }

            /* Annuleren - cancel button behavior */
            app.Tap(c => c.Marked("NIEUWE VERBINDING"));
            app.Tap(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.ClearText(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.EnterText(c => c.Marked("AddNewEnvironment_EnvNameEntry"), TestData.InvalidName);
            app.Tap(c => c.Marked("AddNewEnvironment_WebURLEntry"));
            app.ClearText(c => c.Marked("AddNewEnvironment_WebURLEntry"));
            app.EnterText(c => c.Marked("AddNewEnvironment_WebURLEntry"), TestData.InvalidURL);
            app.DismissKeyboard();
            app.Tap(c => c.Marked("ANNULEREN"));

            AssertForElement(Messages.CancelEnteredURL);

            app.Tap(c => c.Marked("Nee"));

            AssertForElement("AddNewEnvironment_EnvNameEntry");

            app.Tap(c => c.Marked("ANNULEREN"));
            app.Tap(c => c.Marked("Ja"));

            AssertForElement("Applicatie");
        }

        // sprint 9
        public void EditAndDeleteURLFromSettingsPage()
        {
            List<string> urlList = new List<string>();
            urlList.Add("https://lnet.com.companyname.nl/v2.mdesk.api/api1/");
            urlList.Add("hhttps://lnet.com.companyname.nl/v2.mdesk.api/api2/");
            urlList.Add("hhttps://lnet.com.companyname.nl/v2.mdesk.api/api3/");

            List<string> urlNameList = new List<string>();
            urlNameList.Add("valid name 1");
            urlNameList.Add("valid name 2");
            urlNameList.Add("valid name 3");

            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);
            app.WaitForElement(c => c.Marked("Header_Settings_Button"));

            app.Tap(c => c.Marked("Header_Settings_Button"));
            app.Tap(c => c.Marked("Applicatie"));

            for (int i = 0; i < urlList.Count; i++)
            {
                app.Tap(c => c.Marked("NIEUWE VERBINDING"));

                app.Tap(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
                app.ClearText(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
                app.EnterText(c => c.Marked("AddNewEnvironment_EnvNameEntry"), urlNameList[i]);
                app.DismissKeyboard();
                app.Tap(c => c.Marked("AddNewEnvironment_WebURLEntry"));
                app.ClearText(c => c.Marked("AddNewEnvironment_WebURLEntry"));
                app.EnterText(c => c.Marked("AddNewEnvironment_WebURLEntry"), urlList[i]);
                app.DismissKeyboard();
                app.Tap(c => c.Marked("OPSLAAN"));
            }

            // Delete URl 
            app.Tap(c => c.Marked("valid name 1"));
            app.Tap(c => c.Marked("Header_Delete_Button"));

            /*Verify message  "Wilt u deze verbinding definitief verwijderen?" is displayed */
            AssertForElement(Messages.DeleteEnteredURL);
            app.Tap(c => c.Marked("Ja"));

            /*verify "valid name 1" is not displayed after deletion */
            CheckForElementNotPresent("valid name 1");

            // validating 'Nee' option did not perform Deletion
            app.Tap(c => c.Marked("valid name 2"));
            app.Tap(c => c.Marked("Header_Delete_Button"));
            app.Tap(c => c.Marked("Nee"));
            // validate "valid name 2" is exist in the grid
            CheckForElement("valid name 2");
            app.Tap(c => c.Marked("ANNULEREN"));
            // validate "valid name 2" is exist in the Applicatie page
            CheckForElement("valid name 2");

            // edit name and url
            app.Tap(c => c.Marked("valid name 3"));
            app.Tap(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.EnterText(c => c.Marked("AddNewEnvironment_EnvNameEntry"), "_Name field edited");
            app.DismissKeyboard();
            app.Tap(c => c.Marked("AddNewEnvironment_WebURLEntry"));
            app.EnterText(c => c.Marked("AddNewEnvironment_WebURLEntry"), "editedURL");
            app.DismissKeyboard();
            app.Tap(c => c.Marked("OPSLAAN"));
            AssertForElement(Messages.UpdateDataFieldMessage);
            app.Tap(c => c.Marked("Ja"));

            // verify edited name an field is displayed in the Applicatie page
            AssertForElement("valid name 3_Name field edited");
            //Verify edited 'name' and 'URl' is edited succssfully
            app.Tap(c => c.Marked("valid name 3_Name field edited"));
            AssertForElement("valid name 3_Name field edited");
            //AssertForElement("hhttps://lnet.com.companyname.editedURLnl/v2.mdesk.api/api3/");
            app.Tap(C => C.Marked("ANNULEREN"));


        }

        //Sprint 9
        [Test]
        public void EditAndDeleteExistingURLfromSettingsPage()

        {
            List<string> urlList = new List<string>();
            urlList.Add("https://lnet.com.companyname.nl/v2.mdesk.api/api1/");
            urlList.Add("hhttps://lnet.com.companyname.nl/v2.mdesk.api/api2/");


            // List<string> urlNameList = new List<string>();
            // urlNameList.Add("valid name 1");
            //  urlNameList.Add("valid name 2");

            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);
            app.WaitForElement(c => c.Marked("Header_Settings_Button"));

            app.Tap(c => c.Marked("Header_Settings_Button"));
            app.Tap(c => c.Marked("Applicatie"));

            Thread.Sleep(3000);

            AppResult[] elements = app.Query(c => c.Marked(TestData.UrlName));

            if (elements.Count() > 1)
            {
                var editItem = elements[1];
                app.TapCoordinates(editItem.Rect.CenterX, editItem.Rect.CenterY);
            }

            app.WaitForElement(c => c.Marked("Header_Delete_Button"));

            // Delete existing URl 
            app.Tap(c => c.Marked("Header_Delete_Button"));

            // Validating "Actieve verbinding kan niet worden bewerkt" message
            AssertForElement(Messages.DeleteExistingURLMessage);
            app.Tap(c => c.Marked("OK"));

            // Validation for field did not deleted
            AssertForElement("Valid URl for Rotterdam");

            // Validate text field is not editable 
            app.Tap(c => c.Marked("Valid URl for Rotterdam"));

        }

        // sprint 9
        [Test]
        public void ChangeLanguageFromSettingPage()
        {
            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);
            app.WaitForElement(c => c.Marked("Header_Settings_Button"));

            app.Tap(c => c.Marked("Header_Settings_Button"));
            app.Tap(c => c.Marked("Applicatie"));

            // Verify user selected "COMPANYNAME" language is displayed under 'Taal' option
            AssertForElement("COMPANYNAME");
            //app.WaitForElement(c => c.Marked("Header_Settings_Button"));

            app.Tap(c => c.Marked("Taal"));
            // Verify Taalkeuze pop up box is displayed 
            AssertForElement("Taalkeuze");

            app.Tap(c => c.Marked("COMPANYNAME"));
            app.Tap(c => c.Marked("For Gayathri"));
            app.Tap(c => c.Marked("Nederlands"));
            app.Tap(c => c.Marked("OK"));

            // Verify user lastly selected language (Nederlands) is displayed under 'Taal' option 
            AssertForElement("Nederlands");

            // Verify CANCEL/ANNULEREN button beghavior 
            app.Tap(c => c.Marked("Taal"));
            app.Tap(c => c.Marked("COMPANYNAME"));
            app.Tap(c => c.Marked("ANNULEREN"));

            //Verify newly selected language is not displayed in the 'Taal' option after user press cancel button
            AssertForElement("Nederlands");

            app.Tap(c => c.Marked("Header_Back_Button"));
            app.Tap(c => c.Marked("Applicatie"));
            AssertForElement("Nederlands");
        }

        // Sprint 10

        [Test]
        public void ExistingURLOpslaanButtonBehavior()
        {


            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);


            app.WaitForElement(c => c.Marked("Header_Settings_Button"));

            app.Tap(c => c.Marked("Header_Settings_Button"));
            app.Tap(c => c.Marked("Applicatie"));

            Thread.Sleep(3000);

            AppResult[] elements = app.Query(c => c.Marked(TestData.UrlName));

            if (elements.Count() > 1)
            {
                var editItem = elements[1];
                app.TapCoordinates(editItem.Rect.CenterX, editItem.Rect.CenterY);
            }

            //  app.Repl();

            app.WaitForElement(c => c.Marked("OPSLAAN"));


            app.Tap(c => c.Marked("OPSLAAN"));

            // Validating "Actieve verbinding kan niet worden bewerkt" message
            AssertForElement(TestData.UrlName);




            //  app.Tap(c => c.Marked("OK"));

            // Validation for field did not deleted
            //AssertForElement("Valid URl for Rotterdam");

            // Validate text field is not editable 
            // app.Tap(c => c.Marked("Valid URl for Rotterdam"));

        }

        // Sprint 10
        [Test]
        public void CancelOfAddNewURLLink()
        {
            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);
            app.WaitForElement(c => c.Marked("Header_Settings_Button"));

            app.Tap(c => c.Marked("Header_Settings_Button"));
            app.Tap(c => c.Marked("Applicatie"));
            app.WaitForElement(c => c.Marked("NIEUWE VERBINDING"));
            app.Tap(c => c.Marked("NIEUWE VERBINDING"));

            app.Tap(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.ClearText(c => c.Marked("AddNewEnvironment_EnvNameEntry"));
            app.EnterText(c => c.Marked("AddNewEnvironment_EnvNameEntry"), TestData.UrlNewName1);
            app.DismissKeyboard();

            app.Tap(c => c.Marked("AddNewEnvironment_WebURLEntry"));
            app.ClearText(c => c.Marked("AddNewEnvironment_WebURLEntry"));
            app.EnterText(c => c.Marked("AddNewEnvironment_WebURLEntry"), TestData.ValidNewURL1);
            app.DismissKeyboard();
            app.Tap(c => c.Marked("ANNULEREN"));
            /* Verify "Wilt u het scherm sluiten? Wijzigingen gaan verloren? /
            Do you want to close the screen? Changes are lost" message is displayed */
            Thread.Sleep(3000);

            AssertForElement(Messages.CancelEnteredValuewithoutSaving);
            //  Verify  Nee button  click will stay in the same new URl enter page

            app.Tap(c => c.Marked("Nee"));
            AssertForElement("Naam verbinding");
            // Verify

            app.Tap(c => c.Marked("ANNULEREN"));
            // mesage\

            app.Tap(c => c.Marked("Ja"));
            // validation of Applicatie page
            AssertForElement("Applicatie");
        }

        // Sprint 12
        [Test]
        public void CancelOfExistingURL()
        {
            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);
            app.WaitForElement(c => c.Marked("Header_Settings_Button"));
            app.Tap(c => c.Marked("Header_Settings_Button"));

            app.Tap(c => c.Marked("Applicatie"));

            AppResult[] elements = app.Query(c => c.Marked(TestData.UrlName));

            if (elements.Count() > 1)
            {
                var editItem = elements[1];
                app.TapCoordinates(editItem.Rect.CenterX, editItem.Rect.CenterY);

                app.Tap(c => c.Marked("ANNULEREN"));

                Thread.Sleep(3000);

                AssertForElement(Messages.ExistingUrlName);
            }
        }
    }
}
