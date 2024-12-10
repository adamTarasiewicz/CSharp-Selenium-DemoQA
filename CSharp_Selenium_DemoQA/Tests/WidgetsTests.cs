using CSharp_Selenium_DemoQA.Pages.Widgets;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;

namespace CSharp_Selenium_DemoQA.Tests
{
    [TestClass]
    [TestCategory("Widgets Tests")]
    public class WidgetsTests
    {
        private IWebDriver Driver { get; set; } = null!;
        private IWebElement webPageMainHeader => Driver.FindElement(By.XPath("//h1[@class='text-center']"));
        internal TestUser TheTestUser { get; set; } = null!;

        [TestInitialize]
        public void SetupForEverySingleTestMethod()
        {
            Driver = GetChromeDriver();
            Driver.Manage().Window.Maximize();
        }

        private void VerifyPageTitle(string expectedTitle)
        {
            string actualTitle = webPageMainHeader.Text;
            Assert.AreEqual(expectedTitle, actualTitle, $"Expected title '{expectedTitle}' but got '{actualTitle}' instead");
        }

        [TestMethod]
        [Description("Accordian")]
        public void Accordian()
        {
            var accordianPage = new AccordianPage(Driver);
            accordianPage.GoTo();
            VerifyPageTitle("Accordian");

            int heightDifference1 = accordianPage.GetHeightDifferenceAfterClick(accordianPage.Section1Heading, accordianPage.Section1Content);
            Assert.AreNotEqual(0, heightDifference1, "Section 1 did not unfold after the click.");

            int heightDifference2 = accordianPage.GetHeightDifferenceAfterClick(accordianPage.Section2Heading, accordianPage.Section2Content);
            Assert.AreNotEqual(0, heightDifference2, "Section 2 did not unfold after the click.");

            int heightDifference3 = accordianPage.GetHeightDifferenceAfterClick(accordianPage.Section3Heading, accordianPage.Section3Content);
            Assert.AreNotEqual(0, heightDifference3, "Section 3 did not unfold after the click.");
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