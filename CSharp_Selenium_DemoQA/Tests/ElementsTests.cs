using CSharp_Selenium_DemoQA.Pages.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;
using System.Xml.Linq;

namespace CSharp_Selenium_DemoQA.Tests
{
    [TestClass]
    [TestCategory("Elements")]
    public class ElementsTests
    {
        private IWebDriver Driver { get; set; }
        private IWebElement webPageMainHeader => Driver.FindElement(By.XPath("//div[@class='main-header']"));

        [TestInitialize]
        public void SetupForEverySingleTestMethod()
        {
            Driver = GetChromeDriver();
            Driver.Manage().Window.Maximize();
        }

        [TestMethod]
        [Description("Text box")]
        public void TestTextBox()
        {
            var elementsPage = new ElementsPage(Driver);
            elementsPage.GoTo();
            Assert.AreEqual("Elements", webPageMainHeader.Text);

            Driver.FindElement(By.Id("item-0")).Click();

            elementsPage.FillOutFormAndSubmit("John Doe", "my@testing.com", "485 Greenview Drive, Ballston Spa, NY 12020", "Noelle Adams, 6351 Fringilla Avenue, Gardena Colorado 37547, (559) 104-5475");
            Assert.AreEqual("Name:John Doe", elementsPage.Name);
            Assert.AreEqual("Email:my@testing.com", elementsPage.Email);
            Assert.AreEqual("Current Address :485 Greenview Drive, Ballston Spa, NY 12020 ", elementsPage.CurrentAddress, "Element not found");
            Assert.AreEqual("Permananet Address :Noelle Adams, 6351 Fringilla Avenue, Gardena Colorado 37547, (559) 104-5475", elementsPage.PermanentAddress);
        }


        [TestMethod]
        [Description("Check box")]
        public void TestCheckBox()
        {
            var checkBoxPage = new CheckBoxPage(Driver);
            checkBoxPage.GoTo();
            Assert.AreEqual("Check Box", webPageMainHeader.Text);

            checkBoxPage.ExpandAll.Click();
            checkBoxPage.CollapseAll.Click();
            checkBoxPage.ExpandAll.Click();

            checkBoxPage.TreeElements[4].Click();

            Assert.AreEqual(10, checkBoxPage.spanElements.Count);
            checkBoxPage.TreeElements[4].Click();

            checkBoxPage.TreeElements[5].Click();
            Assert.AreEqual(4, checkBoxPage.spanElements.Count);
            checkBoxPage.TreeElements[5].Click();

            checkBoxPage.TreeElements[6].Click();
            Assert.AreEqual(1, checkBoxPage.spanElements.Count);
        }

        [TestMethod]
        public void TestRadioButton()
        {

        }

        [TestMethod]
        public void TestWebTables()
        {

        }

        [TestMethod]
        public void TestButtons()
        {

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
            return new ChromeDriver(outPutDirectory);
        }
    }
}