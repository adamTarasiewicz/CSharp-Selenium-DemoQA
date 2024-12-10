using CSharp_Selenium_DemoQA.Pages.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;

namespace CSharp_Selenium_DemoQA.Tests
{
    [TestClass]
    [TestCategory("Forms")]
    public class FormsTests
    {
        private IWebDriver Driver { get; set; } = null!;
        private IWebElement webPageMainHeader => Driver.FindElement(By.XPath("//h1[@class='text-center']"));
        internal TestUser TheTestUser { get; set; } = null!;

        [TestInitialize]
        public void SetupForEverySingleTestMethod()
        {
            Driver = GetChromeDriver();
            // Driver.Manage().Window.Maximize();

            TheTestUser = new TestUser();
            TheTestUser.FirstName = "Joey";
            TheTestUser.LastName = "Jordison";
            TheTestUser.Email = "joey.jordison@slipknot.com";
            TheTestUser.GenderType = Gender.Male;
            TheTestUser.PhoneNumber = "1234567890";
            TheTestUser.DateOfBirth = "04.26.1975";
            TheTestUser.Subjects = "Computer Science";
            TheTestUser.CurrentAddress = "282 Fairfield Drive Houston, TX 77016";
        }

        private void VerifyPageTitle(string expectedTitle)
        {
            string actualTitle = webPageMainHeader.Text;
            Assert.AreEqual(expectedTitle, actualTitle, $"Expected title '{expectedTitle}' but got '{actualTitle}' instead");
        }

        [TestMethod]
        [Description("Practice form")]
        public void PracticeForm()
        {
            var practiceFormPage = new PracticeFormPage(Driver);
            practiceFormPage.GoTo();
            VerifyPageTitle("Practice Form");

            practiceFormPage.FillOutTheFormAndSubmit(TheTestUser);

            Assert.AreEqual("Thanks for submitting the form", Driver.FindElement(By.XPath("//div[@class='modal-title h4']")).GetAttribute("textContent"));
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
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--window-size=1920,1080");
            return new ChromeDriver(outPutDirectory, chromeOptions);
        }
    }
}