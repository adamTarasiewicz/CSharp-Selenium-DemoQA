using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CSharp_Selenium_DemoQA.Pages.Forms
{
    internal class PracticeFormPage : BasePage
    {
        public PracticeFormPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement FirstNameField => Driver.FindElement(By.Id("firstName"));
        public IWebElement LastNameField => Driver.FindElement(By.Id("lastName"));
        public IWebElement EmailField => Driver.FindElement(By.Id("userEmail"));
        public IWebElement MobilePhoneField => Driver.FindElement(By.Id("userNumber"));
        public IWebElement DateOfBirthField => Driver.FindElement(By.Id("dateOfBirthInput"));
        public IWebElement SubjectsField => Driver.FindElement(By.XPath("//*[@id='subjectsContainer']/div/div[1]"));
        public IWebElement CurrentAddressField => Driver.FindElement(By.Id("currentAddress"));
        public IWebElement MaleGenderRadioButton => Driver.FindElement(By.XPath("//label[@for='gender-radio-1']"));
        public IWebElement SportsCheckBox => Driver.FindElement(By.XPath("//label[@for='hobbies-checkbox-1']"));
        public IWebElement ReadingCheckBox => Driver.FindElement(By.XPath("//label[@for='hobbies-checkbox-2']"));
        public IWebElement MusicCheckBox => Driver.FindElement(By.XPath("//label[@for='hobbies-checkbox-3']"));
        public IWebElement SelectState => Driver.FindElement(By.XPath("//div[@class=' css-1wa3eu0-placeholder' and text()='Select State']/.."));
        public IWebElement SelectStateHaryana => Driver.FindElement(By.XPath("//div[text()='Haryana']"));
        public IWebElement SelectCity => Driver.FindElement(By.XPath("//div[@class=' css-1wa3eu0-placeholder' and text()='Select City']/.."));
        public IWebElement SelectCityKarnal => Driver.FindElement(By.XPath("//div[text()='Karnal']"));
        public IWebElement Submit => Driver.FindElement(By.Id("submit"));

        internal void FillOutTheFormAndSubmit(TestUser user)
        {
            FirstNameField.SendKeys(user.FirstName);
            SubjectsField.Click();

            SendLetterMAndChooseMathAndClickEnter();

            LastNameField.SendKeys(user.LastName);
            EmailField.SendKeys(user.Email);

            SetGender(user);

            MobilePhoneField.SendKeys(user.PhoneNumber);

            DateOfBirthField.Click();
            DateOfBirthField.SendKeys(Keys.Control + "a");
            DateOfBirthField.SendKeys(user.DateOfBirth);
            DateOfBirthField.SendKeys(Keys.Enter);

            // Hobbies checkboxes
            SportsCheckBox.Click();
            ReadingCheckBox.Click();
            MusicCheckBox.Click();

            CurrentAddressField.SendKeys(user.CurrentAddress);

            SelectStateAndCity();

            Submit.Click();
        }

        private void SelectStateAndCity()
        {
            Actions actions = new Actions(Driver);
            actions.Click(SelectState);
            actions.SendKeys("Haryana");
            actions.SendKeys(Keys.Enter).Perform();

            actions.Click(SelectCity);
            actions.SendKeys("Karnal");
            actions.SendKeys(Keys.Enter).Perform();
        }

        private void SendLetterMAndChooseMathAndClickEnter()
        {
            Actions actions = new Actions(Driver);
            actions.SendKeys("M").Perform();
            actions.SendKeys(Keys.Enter).Perform();
        }

        private void SetGender(TestUser user)
        {
            switch (user.GenderType)
            {
                case Gender.Male:
                    MaleGenderRadioButton.Click();
                    break;

                case Gender.Female:
                    break;

                case Gender.Other:
                    break;

                default:
                    break;
            }
        }

        internal void GoTo()
        {
            Driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");
        }
    }
}