using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;

namespace CSharp_Selenium_DemoQA.Tests
{
    [TestClass]
    [TestCategory("Alerts, Frame & Windows")]
    public class AlertsFrameWindowsTests
    {
        private IWebDriver Driver { get; set; } = null!;
        private IWebElement webPageMainHeader => Driver.FindElement(By.XPath("//h1[@class='text-center']"));
        internal TestUser TheTestUser { get; set; } = null!;

        [TestInitialize]
        public void SetupForEverySingleTestMethod()
        {
            Driver = GetChromeDriver();
            Driver.Manage().Window.Maximize();

            TheTestUser = new TestUser();
            TheTestUser.FullName = "Ken Block";
        }

        private void VerifyPageTitle(string expectedTitle)
        {
            string actualTitle = webPageMainHeader.Text;
            Assert.AreEqual(expectedTitle, actualTitle, $"Expected title '{expectedTitle}' but got '{actualTitle}' instead");
        }

        [TestMethod]
        [Description("Browser Windows")]
        public void BrowserWindows()
        {
            var browserWindowsPage = new BrowserWindowsPage(Driver);
            browserWindowsPage.GoTo();
            VerifyPageTitle("Browser Windows");

            browserWindowsPage.CheckNewTab();
        }

        [TestMethod]
        [Description("Alerts")]
        public void Alerts()
        {
            var alertsPage = new AlertsPage(Driver);
            alertsPage.GoTo();
            VerifyPageTitle("Alerts");

            alertsPage.CheckAlerts(TheTestUser);
        }

        [TestMethod]
        [Description("Frames")]
        public void Frames()
        {
            var framesPage = new FramesPage(Driver);
            framesPage.GoTo();
            VerifyPageTitle("Frames");

            string frame1Content = framesPage.GetFrame1Content();
            Assert.AreEqual("This is a sample page", frame1Content, "Frame 1 text mismatch");

            string frame2Content = framesPage.GetFrame2Content();
            Assert.AreEqual("This is a sample page", frame2Content, "Frame 2 text mismatch");
        }

        [TestMethod]
        [Description("Nested Frames")]
        public void NestedFrames()
        {
            var nestedFramesPage = new NestedFramesPage(Driver);
            nestedFramesPage.GoTo();
            VerifyPageTitle("Nested Frames");

            string parentFrameContent = nestedFramesPage.GetParentFrameContent();
            Assert.AreEqual("Parent frame", parentFrameContent, "Parent frame text mismatch");

            string childFrameContent = nestedFramesPage.GetChildFrameContent();
            Assert.AreEqual("Child Iframe", childFrameContent, "Child frame text mismatch");
        }

        [TestMethod]
        [Description("Modal Dialogs")]
        public void ModalDialogs()
        {
            var modalDialogsPage = new ModalDialogsPage(Driver);
            modalDialogsPage.GoTo();
            VerifyPageTitle("Modal Dialogs");

            string smallModalText = modalDialogsPage.GetSmallModalText();
            Assert.AreEqual("Small Modal", smallModalText, "Mismatch in Small Modal Text");
            modalDialogsPage.CloseSmallModalDialog();

            string largeModalText = modalDialogsPage.GetLargeModalText();
            Assert.AreEqual("Large Modal", largeModalText, "Mismatch in Large Modal Text");
            modalDialogsPage.CloseLargeModalDialog();
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
            chromeOptions.AddArgument("--headless"); // unlock for CI
            chromeOptions.AddArgument("--window-size=1920,1080"); // unlock for CI
            return new ChromeDriver(outPutDirectory, chromeOptions);
        }
    }
}