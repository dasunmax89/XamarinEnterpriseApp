using XamarinEnterpriseApp.Xamarin.Core.Tests;
using NUnit.Framework;
using System;
using System.Threading;
using Xamarin.UITest;

namespace XamarinEnterpriseApp.Xamarin.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]

    public class MapMarkerTests : TestBase
    {
        Platform platform;

        public MapMarkerTests(Platform platform)
        {
            this.platform = platform;
        }


        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void DisplayMapMarkersOnTheMap()

        {
            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);

            app.Tap(c => c.Marked("Kaart"));

            app.SwipeRightToLeft(c => c.Marked("MapView_HorizontalScrollView"));
            app.SwipeRightToLeft(c => c.Marked("MapView_HorizontalScrollView"));

            //app.Repl();

            app.Tap(c => c.Marked("NAVIGEER"));
            app.SwipeRightToLeft(c => c.Marked("MapView_HorizontalScrollView"));


            app.Tap(c => c.Marked("NAVIGEER"));

            TimeSpan duration = new TimeSpan(2);

            //  app.PinchToZoomInCoordinates(50.8770020855659f, 5.72053275213948f, duration);
            // app.PinchToZoomIn()

        }

        // Sprint 11
        [Test]
        public void ScrollReportDetails()
        {

            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);

            app.Tap(c => c.Marked("Kaart"));

            string[] viewList = new string[5];

            for (int i = 0; i < viewList.Length; i++)
            {
                app.SwipeRightToLeft(c => c.Marked("MapView_HorizontalScrollView"));
            }

            app.SwipeLeftToRight(c => c.Marked("MapView_HorizontalScrollView"));
            app.Tap(c => c.Marked("NAVIGEER"));
            app.Tap(c => c.Marked("Werkvoorraad"));

            AssertForElement("Mijn Werkvoorraad");

            // verify list

            string[] Listview = new string[5];
            for (int i = 0; i < Listview.Length; i++)
            {
                app.ScrollDown(c => c.Marked("WorkLoadListView"));
            }

        }

        // Sprint 12
        [Test]
        public void CreateReport()
        {
            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);

            app.Tap(c => c.Marked("Nieuwe Melding"));

            app.PinchToZoomOutCoordinates(100, 100, TimeSpan.FromSeconds(1));
            Thread.Sleep(3000);

            app.Tap(c => c.Marked("Deze locatie gebruiken"));

            app.Tap(c => c.Marked("Plaats"));
            Thread.Sleep(2000);
            app.Tap(c => c.Marked("Maastricht"));

            app.Tap(c => c.Marked("OK"));
            // app.Repl();
            app.Tap(c => c.Marked("Locatie"));
            Thread.Sleep(3000);
            app.Tap(c => c.Marked("1 juli-weg")); // remove the  comma in new release

            app.Tap(c => c.Marked("OK"));

            app.Tap(c => c.Marked("TextEditorView"));
            app.ClearText(c => c.Marked("TextEditorView"));
            app.EnterText(c => c.Marked("TextEditorView"), TestData.Ter_hoogte_van);
            app.DismissKeyboard();

            app.Tap(c => c.Marked("VOLGENDE"));

            app.Tap(c => c.Marked("Afval"));
            app.Tap(c => c.Marked("Zwerfvuil (blikjes, flesjes, bladeren)"));
            app.Tap(c => c.Marked("Zwerfvuil"));
            app.Tap(c => c.Marked("VOLGENDE"));

            app.Tap(c => c.Marked("TextEditorView"));
            app.ClearText(c => c.Marked("TextEditorView"));
            app.EnterText(c => c.Marked("TextEditorView"), TestData.descripton);
            app.DismissKeyboard();

            app.Tap(c => c.Marked("KLAAR"));
        }
    }
}

