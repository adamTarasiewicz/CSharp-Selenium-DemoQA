using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CSharp_Selenium_DemoQA.Tests
{
    internal class BrowserWindowsPage : BasePage
    {
        public BrowserWindowsPage(IWebDriver driver) : base(driver)
        {
        }

        // Elements
        public IWebElement NewTabButton => Driver.FindElement(By.Id("tabButton"));
        public IWebElement NewWindowButton => Driver.FindElement(By.Id("windowButton"));
        public IWebElement NewWindowMessageButton => Driver.FindElement(By.Id("messageWindowButton"));
        public IWebElement NewTabButtonAndNewWindowAssert => Driver.FindElement(By.Id("sampleHeading"));

        // Switch to New Window/Tab
        private void SwitchToNewWindow(int initialWindowCount)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.WindowHandles.Count > initialWindowCount);
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
        }

        // Assert Text in New Window/Tab
        private void AssertTextInNewWindow(string expectedText)
        {
            Assert.AreEqual(expectedText, NewTabButtonAndNewWindowAssert.Text, "Text mismatch");
        }

        // Close New Window and Switch to Original
        private void CloseNewWindowAndSwitchToOriginal()
        {
            Driver.Close();
            Driver.SwitchTo().Window(Driver.WindowHandles.First());
        }

        internal void CheckNewTab()
        {
            int initialWindowCount = Driver.WindowHandles.Count;

            // Check New Tab
            NewTabButton.Click();
            SwitchToNewWindow(initialWindowCount);
            AssertTextInNewWindow("This is a sample page");
            Driver.SwitchTo().Window(Driver.WindowHandles.First());

            // Check New Window
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
