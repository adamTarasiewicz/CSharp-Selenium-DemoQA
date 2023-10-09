using OpenQA.Selenium;

namespace CSharp_Selenium_DemoQA.Tests
{
    internal class NestedFramesPage : BasePage
    {
        public NestedFramesPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement ParentIFrame => Driver.FindElement(By.Id("frame1"));
        public IWebElement ChildIFrame => Driver.FindElement(By.XPath("//iframe[@srcdoc='<p>Child Iframe</p>']"));

        public string GetParentFrameContent()
        {
            Driver.SwitchTo().Frame(ParentIFrame);
            string parentContent = Driver.FindElement(By.TagName("body")).Text;
            Driver.SwitchTo().DefaultContent();
            return parentContent;
        }

        public string GetChildFrameContent()
        {
            Driver.SwitchTo().Frame(ParentIFrame);
            Driver.SwitchTo().Frame(ChildIFrame);
            string childContent = Driver.FindElement(By.XPath("//p[text()='Child Iframe']")).Text;
            Driver.SwitchTo().DefaultContent();
            return childContent;
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/nestedframes");
        }
    }
}