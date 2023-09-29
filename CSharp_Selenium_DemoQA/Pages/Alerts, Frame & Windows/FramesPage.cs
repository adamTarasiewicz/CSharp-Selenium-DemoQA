using OpenQA.Selenium;

namespace CSharp_Selenium_DemoQA.Tests
{
    internal class FramesPage : BasePage
    {
        public FramesPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement Frame1 => Driver.FindElement(By.Id("frame1"));
        public IWebElement Frame2 => Driver.FindElement(By.Id("frame2"));

        public string GetFrame1Content()
        {
            Driver.SwitchTo().Frame(Frame1);
            string content = Driver.FindElement(By.Id("sampleHeading")).Text;
            Driver.SwitchTo().DefaultContent();
            return content;
        }

        public string GetFrame2Content()
        {
            Driver.SwitchTo().Frame(Frame2);
            string content = Driver.FindElement(By.Id("sampleHeading")).Text;
            Driver.SwitchTo().DefaultContent();
            return content;
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/frames");
        }
    }
}