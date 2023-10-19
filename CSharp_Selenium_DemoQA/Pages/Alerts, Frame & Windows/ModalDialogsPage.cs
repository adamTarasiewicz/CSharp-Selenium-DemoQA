using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CSharp_Selenium_DemoQA.Tests
{
    internal class ModalDialogsPage : BasePage
    {
        private WebDriverWait wait;

        public ModalDialogsPage(IWebDriver driver) : base(driver)
        {
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
        }

        public IWebElement SmallModal => Driver.FindElement(By.Id("showSmallModal"));
        public IWebElement LargeModal => Driver.FindElement(By.Id("showLargeModal"));
        public IWebElement SmallModalHeader => Driver.FindElement(By.Id("example-modal-sizes-title-sm"));
        public IWebElement LargeModalHeader => Driver.FindElement(By.Id("example-modal-sizes-title-lg"));
        public IWebElement CloseSmallModal => Driver.FindElement(By.Id("closeSmallModal"));
        public IWebElement CloseLargeModal => Driver.FindElement(By.Id("closeLargeModal"));

        public string GetSmallModalText()
        {
            SmallModal.Click();
            wait.Until(ExpectedConditions.TextToBePresentInElement(SmallModalHeader, "Small Modal"));
            return SmallModalHeader.Text;
        }

        public void CloseSmallModalDialog()
        {
            CloseSmallModal.Click();
        }

        public string GetLargeModalText()
        {
            LargeModal.Click();
            wait.Until(ExpectedConditions.TextToBePresentInElement(LargeModalHeader, "Large Modal"));
            return LargeModalHeader.Text;
        }

        public void CloseLargeModalDialog()
        {
            CloseLargeModal.Click();
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/modal-dialogs");
        }
    }
}