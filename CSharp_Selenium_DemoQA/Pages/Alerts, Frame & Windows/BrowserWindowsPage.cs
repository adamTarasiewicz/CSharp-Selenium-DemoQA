using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSharp_Selenium_DemoQA.Tests
{
    internal class BrowserWindowsPage : BasePage
    {
        public BrowserWindowsPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement NewTabButton => Driver.FindElement(By.Id("tabButton"));
        public IWebElement NewWindowButton => Driver.FindElement(By.Id("windowButton"));
        public IWebElement NewWindowMessageButton => Driver.FindElement(By.Id("messageWindowButton"));
        public IWebElement NewTabButtonAndNewWindowAssert => Driver.FindElement(By.Id("sampleHeading"));

        private void SwitchToNewWindow(int initialWindowCount)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.WindowHandles.Count > initialWindowCount);
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
        }

        private void AssertTextInNewWindow(string expectedText)
        {
            Assert.AreEqual(expectedText, NewTabButtonAndNewWindowAssert.Text, "Text mismatch");
        }

        private void CloseNewWindowAndSwitchToOriginal()
        {
            Driver.Close();
            Driver.SwitchTo().Window(Driver.WindowHandles.First());
        }

        internal void CheckNewTab()
        {
            int initialWindowCount = Driver.WindowHandles.Count;

            NewTabButton.Click();
            SwitchToNewWindow(initialWindowCount);
            AssertTextInNewWindow("This is a sample page");
            Driver.SwitchTo().Window(Driver.WindowHandles.First());

            NewWindowButton.Click();
            SwitchToNewWindow(initialWindowCount);
            AssertTextInNewWindow("This is a sample page");
            CloseNewWindowAndSwitchToOriginal();
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/browser-windows");
        }
    }
}