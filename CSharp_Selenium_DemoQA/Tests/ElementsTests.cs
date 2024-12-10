using CSharp_Selenium_DemoQA.Pages.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;


namespace CSharp_Selenium_DemoQA.Tests
{
    [TestClass]
    [TestCategory("Elements")]
    public class ElementsTests
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
            TheTestUser.FirstName = "Ken";
            TheTestUser.LastName = "Block";
            TheTestUser.FullName = "Ken Block";
            TheTestUser.Email = "my@testing.com";
            TheTestUser.Age = "43";
            TheTestUser.Salary = "1000000";
            TheTestUser.Department = "Quality Assurance";
            TheTestUser.CurrentAddress = "485 Greenview Drive, Ballston Spa, NY 12020";
            TheTestUser.PermanentAddress = "Noelle Adams, 6351 Fringilla Avenue, Gardena Colorado 37547, (559) 104-5475";
        }

        private void VerifyPageTitle(string expectedTitle)
        {
            string actualTitle = webPageMainHeader.Text;
            Assert.AreEqual(expectedTitle, actualTitle, $"Expected title '{expectedTitle}' but got '{actualTitle}' instead");
        }

        [TestMethod]
        [Description("Text box")]
        public void TextBox()
        {
            var textBoxPage = new TextBoxPage(Driver);
            textBoxPage.GoTo();
            VerifyPageTitle("Text Box");

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
            VerifyPageTitle("Check Box");

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
            VerifyPageTitle("Radio Button");

            radioButtonPage.Yes.Click();
            var selectedValue = radioButtonPage.YouHaveSelected.GetDomProperty("textContent");
            Assert.AreEqual("Yes", selectedValue);

            radioButtonPage.Impressive.Click();
            var selectedValue2 = radioButtonPage.YouHaveSelected.GetDomProperty("textContent");
            Assert.AreEqual("Impressive", selectedValue2);

            bool noButton = radioButtonPage.No.Displayed;
            Assert.IsFalse(noButton, "Is not false");
        }

        [TestMethod]
        [Description("Web Tables")]
        public void WebTables()
        {
            var webTablesPage = new WebTablesPage(Driver);
            webTablesPage.GoTo();
            VerifyPageTitle("Web Tables");

            webTablesPage.AddNewRecordToTheTableAndSubmit(TheTestUser);
            Assert.AreEqual("Ken", webTablesPage.Cells[21].Text);
            Assert.AreEqual("Block", webTablesPage.Cells[22].Text);

            webTablesPage.DeleteLastRowFromTheTable();
            Assert.IsTrue(string.IsNullOrEmpty(webTablesPage.Cells[21].GetDomProperty("textContent").Trim()));

            webTablesPage.AddNewRecordToTheTableAndSubmit(TheTestUser);

            webTablesPage.EditLastRecordInTheTable();
            Assert.AreEqual("500000", webTablesPage.Cells[25].Text);

            webTablesPage.SearchRecordsInTheTable();
            Assert.AreEqual("Ken", webTablesPage.Cells[0].Text);

            /*foreach (var cell in webTablesPage.Cells)
            {
                Console.WriteLine(cell.Text);
            }*/
            //test
        }

        [TestMethod]
        [Description("Buttons")]
        public void Buttons()
        {
            var buttonsPage = new ButtonsPage(Driver);
            buttonsPage.GoTo();
            VerifyPageTitle("Buttons");

            buttonsPage.DoubleClick();
            Assert.AreEqual("You have done a double click", buttonsPage.DoubleClickMessage.Text);

            buttonsPage.RightClick();
            Assert.AreEqual("You have done a right click", buttonsPage.RightClickMessage.Text);

            buttonsPage.JustClick();
            Assert.AreEqual("You have done a dynamic click", buttonsPage.JustClickMessage.Text);
        }

        [TestMethod]
        [Description("Links")]
        public void Links()
        {
            var linksPage = new LinksPage(Driver);
            linksPage.GoTo();
            VerifyPageTitle("Links");

            linksPage.CheckStatuses();
        }

        [TestMethod]
        [Description("Broken Links - Images")]
        public void BrokenLinksAndImages()
        {
            var brokenLinksPage = new BrokenLinksPage(Driver);
            brokenLinksPage.GoTo();
            VerifyPageTitle("Broken Links - Images");

            brokenLinksPage.IsImageBroken();
            brokenLinksPage.IsLinkBroken();
        }

        [TestMethod]
        [Description("Dynamic Properties")]
        public void DynamicProperties()
        {
            var dynamicPropertiesPage = new DynamicPropertiesPage(Driver);
            dynamicPropertiesPage.GoTo();
            VerifyPageTitle("Dynamic Properties");

            string initialId = dynamicPropertiesPage.GetTextWithRandomIDAttribute("id");
            Driver.Navigate().Refresh();
            string newId = dynamicPropertiesPage.GetTextWithRandomIDAttribute("id");
            Assert.AreNotEqual(initialId, newId, "The element ID did not change after reload.");

            Assert.IsTrue(dynamicPropertiesPage.IsButtonEnabledWithin5Seconds(), "The button is not clickable.");

            string expectedColor = "rgba(220, 53, 69, 1)";
            string actualColor = dynamicPropertiesPage.GetButtonColor();
            Assert.AreEqual(expectedColor, actualColor, "The button text color did not change to expected value.");

            Assert.IsTrue(dynamicPropertiesPage.IsButtonVisibleWithin5Seconds(), "The button is not visible.");
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