using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace MarsAdvancedReqnRollAutomation.Pages.Components.Profile.Certifications
{
    public class CertificationListComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public CertificationListComponent(IWebDriver driver, int timeoutSeconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        }

        private By CertificationsTab => By.XPath("//a[text()='Certifications']");

        // same locator style as old code (header-based)
        private By AddNewButton => By.XPath("//th[contains(text(),'Certificate')]/following-sibling::th//div");

        private By DeleteIconForCertificate(string certificateName) =>
            By.XPath($"//td[text()='{certificateName}']/following-sibling::td//i[contains(@class,'remove')]");

        public void NavigateToCertificationsTab()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(CertificationsTab)).Click();
        }

        public void ClickAddNew()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewButton)).Click();
        }

        public void DeleteCertificate(string certificateName)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(DeleteIconForCertificate(certificateName))).Click();
        }
    }
}
