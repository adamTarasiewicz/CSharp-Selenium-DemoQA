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
        internal TestUser TheTestUser { get; set; }

        [TestInitialize]
        public void SetupForEverySingleTestMethod()
        {
            Driver = GetChromeDriver();
            Driver.Manage().Window.Maximize();

            TheTestUser = new TestUser();
            TheTestUser.FullName = "Ken Block";
            TheTestUser.Email = "my@testing.com";
            TheTestUser.CurrentAddress = "485 Greenview Drive, Ballston Spa, NY 12020";
            TheTestUser.PermanentAddress = "Noelle Adams, 6351 Fringilla Avenue, Gardena Colorado 37547, (559) 104-5475";
        }

        [TestMethod]
        [Description("Text box")]
        public void TextBox()
        {
            var textBoxPage = new TextBoxPage(Driver);
            textBoxPage.GoTo();
            Assert.AreEqual("Elements", webPageMainHeader.Text);

            Driver.FindElement(By.Id("item-0")).Click();

            textBoxPage.FillOutFormAndSubmit(TheTestUser);
            Assert.AreEqual("Name:Ken Block", textBoxPage.Name);
            Assert.AreEqual("Email:my@testing.com", textBoxPage.Email);
            Assert.AreEqual("Current Address :485 Greenview Drive, Ballston Spa, NY 12020 ", textBoxPage.CurrentAddress, "Element not found");
            Assert.AreEqual("Permananet Address :Noelle Adams, 6351 Fringilla Avenue, Gardena Colorado 37547, (559) 104-5475", textBoxPage.PermanentAddress);
        }


        [TestMethod]
        [Description("Check box")]
        public void CheckBox()
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
        [Description("Radio Button")]
        public void RadioButton()
        {
            var radioButtonPage = new RadioButtonPage(Driver);
            radioButtonPage.GoTo();
            Assert.AreEqual("Radio Button", webPageMainHeader.Text);

            radioButtonPage.Yes.Click();
            var selectedValue = radioButtonPage.YouHaveSelected.GetAttribute("textContent");
            Assert.AreEqual("Yes", selectedValue);

            radioButtonPage.Impressive.Click();
            var selectedValue2 = radioButtonPage.YouHaveSelected.GetAttribute("textContent");
            Assert.AreEqual("Impressive", selectedValue2);

            bool noButton = radioButtonPage.No.Displayed;
            Assert.IsFalse(noButton, "Is not false");
        }

        [TestMethod]
        public void WebTables()
        {

        }

        [TestMethod]
        public void Buttons()
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