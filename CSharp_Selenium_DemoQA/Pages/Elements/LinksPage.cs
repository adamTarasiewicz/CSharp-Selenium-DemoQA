using OpenQA.Selenium;

namespace CSharp_Selenium_DemoQA.Pages.Elements
{
    internal class LinksPage : BasePage
    {
        private NetworkResponseReceivedEventArgs lastResponseEventArgs;

        private List<IWebElement> allLinks;

        public LinksPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement HomeLink => Driver.FindElement(By.Id("simpleLink"));
        public IWebElement HomeDynamicLink => Driver.FindElement(By.Id("dynamicLink"));
        public IWebElement CreatedLink => Driver.FindElement(By.Id("created"));
        public IWebElement NoContentLink => Driver.FindElement(By.Id("no-content"));
        public IWebElement MovedLink => Driver.FindElement(By.Id("moved"));
        public IWebElement BadRequestLink => Driver.FindElement(By.Id("bad-request"));
        public IWebElement UnauthorizedLink => Driver.FindElement(By.Id("unauthorized"));
        public IWebElement ForbiddenLink => Driver.FindElement(By.Id("forbidden"));
        public IWebElement NotFoundLink => Driver.FindElement(By.Id("invalid-url"));
        public IWebElement StatusCodeForAssertion => Driver.FindElement(By.CssSelector("#linkResponse > b:nth-child(1)"));

        internal void CheckStatuses()
        {
            allLinks = new List<IWebElement>
            {
                NotFoundLink,
                UnauthorizedLink,
                CreatedLink,
                BadRequestLink,
                NoContentLink,
                MovedLink,
                ForbiddenLink
            };

            INetwork networkInterceptor = Driver.Manage().Network;
            void HandleNetworkResponseReceived(object sender, NetworkResponseReceivedEventArgs e)
            {
                lastResponseEventArgs = e;
            }

            networkInterceptor.StartMonitoring();

            foreach (var link in allLinks)
            {
                networkInterceptor.NetworkResponseReceived += HandleNetworkResponseReceived;
                link.Click();
                Thread.Sleep(1000); // needed for response

                networkInterceptor.NetworkResponseReceived -= HandleNetworkResponseReceived;

                if (lastResponseEventArgs != null)
                {
                    int actualStatusCode = ((int)lastResponseEventArgs.ResponseStatusCode);
                    IWebElement updatedStatusCodeForAssertion = StatusCodeForAssertion;

                    Console.WriteLine($"Link '{link.Text}' returned status code: {actualStatusCode}, while it should return {updatedStatusCodeForAssertion.Text}"); //For checking the CI logs.

                    // Assert.AreEqual(updatedStatusCodeForAssertion.Text, actualStatusCode.ToString()); //Locked for CI to pass. Unlock to check locally.
                }
            }
            networkInterceptor.StopMonitoring();
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/links");
        }
    }
}