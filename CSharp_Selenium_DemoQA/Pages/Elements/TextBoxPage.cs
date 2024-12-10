using OpenQA.Selenium;

namespace CSharp_Selenium_DemoQA.Pages.Elements
{
    internal class TextBoxPage : BasePage
    {
        public TextBoxPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement FullNameField => Driver.FindElement(By.Id("userName"));
        public IWebElement EmailField => Driver.FindElement(By.Id("userEmail"));
        public IWebElement CurrentAddressField => Driver.FindElement(By.Id("currentAddress"));
        public IWebElement PermanentAddressField => Driver.FindElement(By.Id("permanentAddress"));
        public string Name => Driver.FindElement(By.XPath("//p[@id='name']")).GetDomProperty("textContent");
        public string Email => Driver.FindElement(By.XPath("//p[@id='email']")).GetDomProperty("textContent");
        public string CurrentAddress => Driver.FindElement(By.XPath("//p[@id='currentAddress']")).GetDomProperty("textContent");
        public string PermanentAddress => Driver.FindElement(By.XPath("//p[@id='permanentAddress']")).GetDomProperty("textContent");
        public IWebElement Submit => Driver.FindElement(By.Id("submit"));

        internal void FillOutFormAndSubmit(TestUser user)
        {
            FullNameField.SendKeys(user.FullName);
            EmailField.SendKeys(user.Email);
            CurrentAddressField.SendKeys(user.CurrentAddress);
            PermanentAddressField.SendKeys(user.PermanentAddress);

            Submit.Click();
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/text-box");
        }
    }
}