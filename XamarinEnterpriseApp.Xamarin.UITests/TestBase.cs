using System.Linq;
using XamarinEnterpriseApp.Xamarin.Core.Tests;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace XamarinEnterpriseApp.Xamarin.UITests
{
    public class TestBase
    {
        protected IApp app;

        public void LoginToApp()
        {
            string username = TestData.UserName;
            string password = TestData.Password;

            TestHelper.Login(username, password, app);
        }

        public bool CheckForElement(string elementName)
        {
            AppResult[] elements = app.WaitForElement(c => c.Marked(elementName));

            return elements.Any();
        }

        public bool CheckForElementNotPresent(string elementName)
        {
            AppResult[] elements = app.Query(c => c.Marked(elementName));

            return elements == null || !elements.Any();
        }

        public bool AssertForElement(string elementName)
        {
            bool isPresent = CheckForElement(elementName);

            Assert.IsTrue(isPresent);

            return isPresent;
        }
    }
}
