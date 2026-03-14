using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MarsAdvancedReqnRollAutomation.Pages.Components.Profile.Certifications
{
    public class CertificationFormComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public CertificationFormComponent(IWebDriver driver, int timeoutSeconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        }

        private By CertificationNameInput => By.Name("certificationName");
        private By CertificationFromInput => By.Name("certificationFrom");
        private By CertificationYearDropdown => By.Name("certificationYear");

        private By AddButton => By.XPath("//input[@value='Add']");

        public void EnterCertificate(string certificate)
        {
            var el = _wait.Until(ExpectedConditions.ElementIsVisible(CertificationNameInput));
            el.Clear();
            el.SendKeys(certificate);
        }

        public void EnterCertifiedFrom(string from)
        {
            var el = _wait.Until(ExpectedConditions.ElementIsVisible(CertificationFromInput));
            el.Clear();
            el.SendKeys(from);
        }

        public void SelectYear(string year)
        {
            var dropdown = _wait.Until(ExpectedConditions.ElementIsVisible(CertificationYearDropdown));
            new SelectElement(dropdown).SelectByText(year);
        }

        public void ClickAdd()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(AddButton)).Click();
        }
    }
}
