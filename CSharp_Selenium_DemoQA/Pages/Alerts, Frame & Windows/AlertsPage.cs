using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CSharp_Selenium_DemoQA.Tests
{
    internal class AlertsPage : BasePage
    {
        public AlertsPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement FirstAlertButton => Driver.FindElement(By.Id("alertButton"));
        public IWebElement SecondTimerAlertButton => Driver.FindElement(By.Id("timerAlertButton"));
        public IWebElement ThirdConfirmButton => Driver.FindElement(By.Id("confirmButton"));
        public IWebElement FourthPromtButton => Driver.FindElement(By.Id("promtButton"));
        public IWebElement ThirdConfirmButtonAssert => Driver.FindElement(By.Id("confirmResult"));
        public IWebElement FourthPromtButtonAssert => Driver.FindElement(By.Id("promptResult"));

        internal void CheckAlerts(TestUser user)
        {
            //1 FirstAlertButton
            FirstAlertButton.Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = Driver.SwitchTo().Alert();

            string alertText = alert.Text;
            Assert.AreEqual("You clicked a button", alertText, "Alert text message mismatch");
            alert.Accept();

            Driver.SwitchTo().DefaultContent();



            //2 SecondTimerAlertButton
            SecondTimerAlertButton.Click();
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert secondAlert = Driver.SwitchTo().Alert();

            string secondAlertText = secondAlert.Text;
            Assert.AreEqual("This alert appeared after 5 seconds", secondAlertText, "Alert text message mismatch");
            secondAlert.Accept();

            Driver.SwitchTo().DefaultContent();



            //3 ThirdConfirmButton
            ThirdConfirmButton.Click();
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert thirdAlertOK = Driver.SwitchTo().Alert();
            thirdAlertOK.Accept();

            string thirdAlertOKText = ThirdConfirmButtonAssert.Text;
            Assert.IsTrue(thirdAlertOKText.Contains("Ok"), "The expected text 'Ok' was not found in the span element.");
            

            ThirdConfirmButton.Click();
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert thirdAlertCancel = Driver.SwitchTo().Alert();
            thirdAlertCancel.Dismiss();

            string thirdAlertCancelText = ThirdConfirmButtonAssert.Text;
            Assert.IsTrue(thirdAlertCancelText.Contains("Cancel"), $"The expected text 'Cancel' was not found in the span element. Found {thirdAlertCancelText} instad.");
            
            Driver.SwitchTo().DefaultContent();


            //4 FourthPromtButton
            FourthPromtButton.Click();
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert fourthAlert = Driver.SwitchTo().Alert();
            
            fourthAlert.SendKeys(user.FullName);

            fourthAlert.Accept();
            
            string fourthAlertOKText = FourthPromtButtonAssert.Text;
            Assert.IsTrue(fourthAlertOKText.Contains("Ken Block"), $"The expected text 'Ken Block' was not found in the span element. Found {fourthAlertOKText} instead.");
            Driver.SwitchTo().DefaultContent();

        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/alerts");
        }
    }
}