using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace CSharp_Selenium_DemoQA.Pages.Elements
{
    internal class CheckBoxPage : BasePage
    {
        public CheckBoxPage(IWebDriver driver) : base(driver)
        {
        }


        public IWebElement ExpandAll => Driver.FindElement(By.XPath("//button[@aria-label='Expand all']"));
        public IWebElement CollapseAll => Driver.FindElement(By.XPath("//button[@aria-label='Collapse all']"));
        public ReadOnlyCollection<IWebElement> TreeElements => Driver.FindElements(By.XPath("//span[@class='rct-text']//span[@class='rct-title']"));
        public IWebElement Result => Driver.FindElement(By.Id("result"));
        public ReadOnlyCollection<IWebElement> spanElements => Result.FindElements(By.XPath("//span[@class='text-success']"));



        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/checkbox");
        }
    }
}