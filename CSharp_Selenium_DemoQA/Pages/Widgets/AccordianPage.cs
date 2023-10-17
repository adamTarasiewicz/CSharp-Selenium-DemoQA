using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;

namespace CSharp_Selenium_DemoQA.Pages.Widgets
{
    internal class AccordianPage : BasePage
    {
        public AccordianPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement Section1Heading => Driver.FindElement(By.Id("section1Heading"));
        public IWebElement Section1Content => Driver.FindElement(By.XPath("//div[@id='section1Heading']/following-sibling::div[1]"));
        public IWebElement Section2Heading => Driver.FindElement(By.Id("section2Heading"));
        public IWebElement Section2Content => Driver.FindElement(By.XPath("//div[@id='section2Heading']/following-sibling::div[1]"));
        public IWebElement Section3Heading => Driver.FindElement(By.Id("section3Heading"));
        public IWebElement Section3Content => Driver.FindElement(By.XPath("//div[@id='section3Heading']/following-sibling::div[1]"));

        internal int GetHeightDifferenceAfterClick(IWebElement heading, IWebElement content)
        {
            int initialHeight = Convert.ToInt32(content.GetAttribute("clientHeight"));
            heading.Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => Convert.ToInt32(content.GetAttribute("clientHeight")) != initialHeight);
            return Convert.ToInt32(content.GetAttribute("clientHeight")) - initialHeight;
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/accordian");
        }
    }
}