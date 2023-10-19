using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CSharp_Selenium_DemoQA.Pages.Elements
{
    internal class DynamicPropertiesPage : BasePage
    {
        public DynamicPropertiesPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement TextWithRandomID => Driver.FindElement(By.XPath("//button[@id='enableAfter']/preceding-sibling::p"));
        public IWebElement WillEnable5SecondsButton => Driver.FindElement(By.Id("enableAfter"));
        public IWebElement ColorChangeButton => Driver.FindElement(By.Id("colorChange"));
        public IWebElement VisibleAfter5SecondsButton => Driver.FindElement(By.Id("visibleAfter"));

        private WebDriverWait wait => new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

        public string GetTextWithRandomIDAttribute(string attributeName)
        {
            return TextWithRandomID.GetAttribute(attributeName);
        }

        public bool IsButtonEnabledWithin5Seconds()
        {
            return wait.Until(ExpectedConditions.ElementToBeClickable(WillEnable5SecondsButton)) != null;
        }

        public string GetButtonColor()
        {
            wait.Until(d => d.FindElement(By.Id("colorChange")).GetAttribute("class").Contains("text-danger"));
            return ColorChangeButton.GetCssValue("color");
        }

        public bool IsButtonVisibleWithin5Seconds()
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(By.Id("visibleAfter"))) != null;
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/dynamic-properties");
        }
    }
}