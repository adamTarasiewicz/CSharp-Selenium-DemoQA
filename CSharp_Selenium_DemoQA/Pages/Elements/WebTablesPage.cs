using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace CSharp_Selenium_DemoQA.Pages.Elements
{
    internal class WebTablesPage : BasePage
    {
        public WebTablesPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement AddButton => Driver.FindElement(By.Id("addNewRecordButton"));
        public IWebElement SearchInputField => Driver.FindElement(By.Id("searchBox"));
        public IWebElement FirstNameField => Driver.FindElement(By.Id("firstName"));
        public IWebElement LastNameField => Driver.FindElement(By.Id("lastName"));
        public IWebElement EmailField => Driver.FindElement(By.Id("userEmail"));
        public IWebElement AgeField => Driver.FindElement(By.Id("age"));
        public IWebElement SalaryField => Driver.FindElement(By.Id("salary"));
        public IWebElement DepartmentField => Driver.FindElement(By.Id("department"));
        public IWebElement SubmitButton => Driver.FindElement(By.Id("submit"));
        public IWebElement DeleteRowFromTheTableButton => Driver.FindElement(By.Id("delete-record-4"));
        public IWebElement EditRowFromTheTableButton => Driver.FindElement(By.Id("edit-record-4"));
        public ReadOnlyCollection<IWebElement> Rows => Driver.FindElements(By.XPath("//div[@class='rt-tbody']//div[@role='row']"));
        public IWebElement Container => Driver.FindElement(By.XPath("//div[@class='rt-tr -odd']/../.."));
        public IList<IWebElement> Cells => Container.FindElements(By.XPath("//div[@class='rt-td']"));
        

        internal void AddNewRecordToTheTableAndSubmit(TestUser user)
        {
            AddButton.Click();
            FirstNameField.SendKeys(user.FirstName);
            LastNameField.SendKeys(user.LastName);
            EmailField.SendKeys(user.Email);
            AgeField.SendKeys(user.Age);
            SalaryField.SendKeys(user.Salary);
            DepartmentField.SendKeys(user.Department);

            SubmitButton.Click();
        }

        internal void DeleteLastRowFromTheTable()
        {
            DeleteRowFromTheTableButton.Click();
        }

        internal void EditLastRecordInTheTable()
        {
            EditRowFromTheTableButton.Click();
            SalaryField.Clear();
            SalaryField.SendKeys("500000");
            SubmitButton.Click();  
        }

        internal void SearchRecordsInTheTable()
        {
            SearchInputField.Clear();
            SearchInputField.SendKeys("Block");
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/webtables");
        }
    }
}