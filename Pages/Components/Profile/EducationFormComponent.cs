using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MarsAdvancedReqnRollAutomation.Pages.Components.Profile
{
    public class EducationFormComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public EducationFormComponent(IWebDriver driver, int timeoutSeconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        }

        private By UniversityField => By.Name("instituteName");
        private By DegreeField => By.Name("degree");
        private By CountryDropdown => By.Name("country");
        private By TitleDropdown => By.Name("title");
        private By YearDropdown => By.Name("yearOfGraduation");

        private By AddButton =>
            By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/div/div[3]/div/input[1]");

        public void EnterUniversity(string university)
        {
            var field = _wait.Until(ExpectedConditions.ElementIsVisible(UniversityField));
            field.Clear();
            field.SendKeys(university);
        }

        public void EnterDegree(string degree)
        {
            var field = _wait.Until(ExpectedConditions.ElementIsVisible(DegreeField));
            field.Clear();
            field.SendKeys(degree);
        }

        public void SelectCountry(string country)
        {
            new SelectElement(_wait.Until(ExpectedConditions.ElementIsVisible(CountryDropdown)))
                .SelectByText(country);
        }

        public void SelectTitle(string title)
        {
            new SelectElement(_wait.Until(ExpectedConditions.ElementIsVisible(TitleDropdown)))
                .SelectByText(title);
        }

        public void SelectYear(string year)
        {
            new SelectElement(_wait.Until(ExpectedConditions.ElementIsVisible(YearDropdown)))
                .SelectByText(year);
        }

        public void ClickAdd()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(AddButton)).Click();
        }
    }
}
