using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;

namespace CSharp_Selenium_DemoQA.Pages.Elements
{
    internal class ButtonsPage : BasePage
    {
        private Actions actions;
        public ButtonsPage(IWebDriver driver) : base(driver)
        {
            actions = new Actions(Driver);
        }

        private IWebElement DoubleClickMeButton => Driver.FindElement(By.Id("doubleClickBtn"));
        private IWebElement RightClickMeButton => Driver.FindElement(By.Id("rightClickBtn"));
        private IWebElement ClickMeButton => Driver.FindElement(By.XPath("//button[text()='Click Me']"));
        public IWebElement DoubleClickMessage => Driver.FindElement(By.Id("doubleClickMessage"));
        public IWebElement RightClickMessage => Driver.FindElement(By.Id("rightClickMessage"));
        public IWebElement JustClickMessage => Driver.FindElement(By.Id("dynamicClickMessage"));


        internal void DoubleClick()
        {
            actions.DoubleClick(DoubleClickMeButton).Perform();
        }
        internal void RightClick()
        {
            actions.ContextClick(RightClickMeButton).Perform();
        }

        internal void JustClick()
        {
            actions.Click(ClickMeButton).Perform();
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/buttons");
        }
    }
}