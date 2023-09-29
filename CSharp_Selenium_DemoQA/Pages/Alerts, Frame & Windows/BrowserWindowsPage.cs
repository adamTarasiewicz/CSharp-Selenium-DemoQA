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

        internal void CheckNewTab()
        {
            int initialWindowCount = Driver.WindowHandles.Count;

            NewTabButton.Click();

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.WindowHandles.Count > initialWindowCount);

            if (Driver.WindowHandles.Count > initialWindowCount)
            {
                // Switch to the new tab
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                Assert.AreEqual("This is a sample page", NewTabButtonAndNewWindowAssert.Text, "Text mismatch");
                // Switch back to the original tab
                Driver.SwitchTo().Window(Driver.WindowHandles.First());
            }

            NewWindowButton.Click();
            wait.Until(d => d.WindowHandles.Count > initialWindowCount);

            if (Driver.WindowHandles.Count > initialWindowCount)
            {
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());

                Assert.AreEqual("This is a sample page", NewTabButtonAndNewWindowAssert.Text, "Text mismatch");

                Driver.Close();
                Driver.SwitchTo().Window(Driver.WindowHandles.First());
            }
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/browser-windows");
        }
    }
}