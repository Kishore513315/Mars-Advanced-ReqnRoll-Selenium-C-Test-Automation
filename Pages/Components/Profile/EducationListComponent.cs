using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MarsAdvancedReqnRollAutomation.Pages.Components.Profile
{
    public class EducationListComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public EducationListComponent(IWebDriver driver, int timeoutSeconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        }

        private By EducationTab =>
            By.XPath("//a[text()='Education']");

        private By AddNewButton =>
            By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/thead/tr/th[6]/div");

        private By DeleteButton =>
            By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody/tr/td[6]/span[2]/i");

        public void NavigateToEducationTab()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(EducationTab)).Click();
        }

        public void ClickAddNew()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewButton)).Click();
        }

        public void ClickDelete()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(DeleteButton)).Click();
        }
    }
}
