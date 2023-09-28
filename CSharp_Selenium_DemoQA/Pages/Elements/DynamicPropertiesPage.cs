using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;

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


        internal void CheckTextHasDifferentIDEachReaload()
        {
            string initialId = TextWithRandomID.GetAttribute("id");
            Driver.Navigate().Refresh();
            string newId = TextWithRandomID.GetAttribute("id");

            Assert.AreNotEqual(initialId, newId, "The element ID did not change after reload.");
        }

        internal void CheckButtonEnabledWithin5Seconds()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement willEnable5SecondsButton = wait.Until(ExpectedConditions.ElementToBeClickable(WillEnable5SecondsButton));
            
            Assert.IsNotNull(willEnable5SecondsButton, "The button is not clickable.");
        }

        internal void CheckButtonColorChange()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10)); // Adjust the timeout as needed
            wait.Until(d =>
            {
                IWebElement btn = d.FindElement(By.Id("colorChange"));
                return btn.GetAttribute("class").Contains("text-danger");
            });
            string newColor = ColorChangeButton.GetCssValue("color");

            Assert.AreEqual("rgba(220, 53, 69, 1)", newColor, "The button text color did not change to expected value.");
        }

        internal void CheckButtonVisibleWithin5Seconds()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement willBeVisible5SecondsButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("visibleAfter")));
            
            Assert.IsNotNull(willBeVisible5SecondsButton, "The button is not visible.");
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/dynamic-properties");
        }
    }
}