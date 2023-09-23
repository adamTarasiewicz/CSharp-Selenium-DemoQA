using OpenQA.Selenium;

namespace CSharp_Selenium_DemoQA
{
    internal class BasePage
    {
        protected IWebDriver Driver { get; set; }

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}