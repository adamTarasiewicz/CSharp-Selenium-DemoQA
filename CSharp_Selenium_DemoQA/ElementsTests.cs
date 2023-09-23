using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;
using System.Xml.Linq;

namespace CSharp_Selenium_DemoQA
{
    [TestClass]
    [TestCategory("Elements")]
    public class ElementsTests
    {
        private IWebDriver Driver { get; set; }

        [TestMethod]
        // text box
        public void TestTextBox()
        {
            var elementsPage = new ElementsPage(Driver);
            elementsPage.GoTo();
            Assert.AreEqual("Elements", Driver.FindElement(By.XPath("//div[@class='main-header']")).Text);

            Driver.FindElement(By.Id("item-0")).Click();

            elementsPage.FillOutFormAndSubmit("John Doe", "my@testing.com", "485 Greenview Drive, Ballston Spa, NY 12020", "Noelle Adams, 6351 Fringilla Avenue, Gardena Colorado 37547, (559) 104-5475");
            Assert.AreEqual("Name:John Doe", elementsPage.Name);
            Assert.AreEqual("Email:my@testing.com", elementsPage.Email);
            Assert.AreEqual("Current Address :485 Greenview Drive, Ballston Spa, NY 12020 ", elementsPage.CurrentAddress, "Element not found");
            Assert.AreEqual("Permananet Address :Noelle Adams, 6351 Fringilla Avenue, Gardena Colorado 37547, (559) 104-5475", elementsPage.PermanentAddress);
        }


        [TestInitialize]
        public void SetupForEverySingleTestMethod()
        {
            Driver = GetChromeDriver();
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