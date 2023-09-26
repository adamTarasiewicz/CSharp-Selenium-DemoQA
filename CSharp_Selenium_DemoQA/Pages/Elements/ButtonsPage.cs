using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CSharp_Selenium_DemoQA.Pages.Elements
{
    internal class ButtonsPage : BasePage
    {
        public ButtonsPage(IWebDriver driver) : base(driver)
        { 
        }

        public IWebElement DoubleClickMeButton => Driver.FindElement(By.Id("doubleClickBtn"));
        public IWebElement RightClickMeButton => Driver.FindElement(By.Id("rightClickBtn"));
        public IWebElement ClickMeButton => Driver.FindElement(By.XPath("//button[text()='Click Me']"));
        public IWebElement DoubleClickMessage => Driver.FindElement(By.Id("doubleClickMessage"));
        public IWebElement RightClickMessage => Driver.FindElement(By.Id("rightClickMessage"));
        public IWebElement JustClickMessage => Driver.FindElement(By.Id("dynamicClickMessage"));


        internal void DoubleClick()
        {
            Actions actions = new Actions(Driver);
            actions.DoubleClick(DoubleClickMeButton).Perform();
        }
        internal void RightClick()
        {
            Actions actions = new Actions(Driver);
            actions.ContextClick(RightClickMeButton).Perform();
        }

        internal void JustClick()
        {
            Actions actions = new Actions(Driver);
            actions.Click(ClickMeButton).Perform();
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/buttons");
        }
    }
}