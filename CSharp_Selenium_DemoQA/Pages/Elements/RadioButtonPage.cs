using OpenQA.Selenium;

namespace CSharp_Selenium_DemoQA.Pages.Elements
{
    internal class RadioButtonPage : BasePage
    {
        public RadioButtonPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement Yes => Driver.FindElement(By.XPath("//label[@for='yesRadio']"));
        public IWebElement Impressive => Driver.FindElement(By.XPath("//label[@for='impressiveRadio']"));
        public IWebElement No => Driver.FindElement(By.Id("noRadio"));
        public IWebElement YouHaveSelected => Driver.FindElement(By.XPath("//p[@class='mt-3']//span[@class='text-success']"));

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/radio-button");
        }
    }
}