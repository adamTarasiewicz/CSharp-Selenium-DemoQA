using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace CSharp_Selenium_DemoQA.Tests
{
    internal class AlertsPage : BasePage
    {
        public AlertsPage(IWebDriver driver) : base(driver)
        {
        }

        // Elements
        public IWebElement FirstAlertButton => Driver.FindElement(By.Id("alertButton"));
        public IWebElement SecondTimerAlertButton => Driver.FindElement(By.Id("timerAlertButton"));
        public IWebElement ThirdConfirmButton => Driver.FindElement(By.Id("confirmButton"));
        public IWebElement FourthPromtButton => Driver.FindElement(By.Id("promtButton"));
        public IWebElement ThirdConfirmButtonAssert => Driver.FindElement(By.Id("confirmResult"));
        public IWebElement FourthPromtButtonAssert => Driver.FindElement(By.Id("promptResult"));

        // Wait for Alert and Return
        private IAlert WaitForAlert()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.AlertIsPresent());
            return Driver.SwitchTo().Alert();
        }

        // Assert Alert Text
        private void AssertAlertText(IAlert alert, string expectedText)
        {
            string alertText = alert.Text;
            Assert.AreEqual(expectedText, alertText, "Alert text message mismatch");
        }

        internal void CheckAlerts(TestUser user)
        {
            // FirstAlertButton
            FirstAlertButton.Click();
            IAlert alert = WaitForAlert();
            AssertAlertText(alert, "You clicked a button");
            alert.Accept();

            // SecondTimerAlertButton
            SecondTimerAlertButton.Click();
            alert = WaitForAlert();
            AssertAlertText(alert, "This alert appeared after 5 seconds");
            alert.Accept();

            // ThirdConfirmButton - OK
            ThirdConfirmButton.Click();
            alert = WaitForAlert();
            alert.Accept();
            Assert.IsTrue(ThirdConfirmButtonAssert.Text.Contains("Ok"), "The expected text 'Ok' was not found in the span element.");

            // ThirdConfirmButton - Cancel
            ThirdConfirmButton.Click();
            alert = WaitForAlert();
            alert.Dismiss();
            Assert.IsTrue(ThirdConfirmButtonAssert.Text.Contains("Cancel"), $"The expected text 'Cancel' was not found in the span element. Found {ThirdConfirmButtonAssert.Text} instead.");

            // FourthPromtButton
            FourthPromtButton.Click();
            alert = WaitForAlert();
            alert.SendKeys(user.FullName);
            alert.Accept();
            Assert.IsTrue(FourthPromtButtonAssert.Text.Contains(user.FullName), $"The expected text '{user.FullName}' was not found in the span element. Found {FourthPromtButtonAssert.Text} instead.");
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/alerts");
        }
    }
}
