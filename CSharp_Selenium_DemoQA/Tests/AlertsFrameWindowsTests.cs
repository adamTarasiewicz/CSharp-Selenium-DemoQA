using CSharp_Selenium_DemoQA.Pages.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Remote;
using System.Reflection;
using System.Xml.Linq;

namespace CSharp_Selenium_DemoQA.Tests
{
    [TestClass]
    [TestCategory("Alerts, Frame & Windows")]
    public class AlertsFrameWindowsTests
    {
        private IWebDriver Driver { get; set; }
        private IWebElement webPageMainHeader => Driver.FindElement(By.XPath("//div[@class='main-header']"));
        internal TestUser TheTestUser { get; set; }

        [TestInitialize]
        public void SetupForEverySingleTestMethod()
        {
            Driver = GetChromeDriver();
            Driver.Manage().Window.Maximize();

            TheTestUser = new TestUser();
            TheTestUser.FullName = "Ken Block";
        }

        [TestMethod]
        [Description("Browser Windows")]
        public void BrowserWindows()
        {
            var browserWindowsPage = new BrowserWindowsPage(Driver);
            browserWindowsPage.GoTo();
            Assert.AreEqual("Browser Windows", webPageMainHeader.Text);

            browserWindowsPage.CheckNewTab();
        }

        [TestMethod]
        [Description("Alerts")]
        public void Alerts()
        {
            var alertsPage = new AlertsPage(Driver);
            alertsPage.GoTo();
            Assert.AreEqual("Alerts", webPageMainHeader.Text);

            alertsPage.CheckAlerts(TheTestUser);
        }



        [TestCleanup]
        public void CleanUpAfterEveryTestMethod()
        {
            Driver.Close();
            Driver.Quit();
        }

        private IWebDriver GetChromeDriver()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("--headless"); // unlock for CI
            //chromeOptions.AddArgument("--window-size=1920,1080"); // unlock for CI
            return new ChromeDriver(outPutDirectory, chromeOptions);
        }
    }
}