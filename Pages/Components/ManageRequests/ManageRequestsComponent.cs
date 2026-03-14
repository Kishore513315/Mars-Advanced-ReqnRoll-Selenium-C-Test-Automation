using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace MarsAdvancedReqnRollAutomation.Pages.Components.ManageRequests
{
    public class ManageRequestsComponent
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public ManageRequestsComponent(IWebDriver driver, int timeoutInSeconds = 10)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        private By ManageRequestsTab => By.XPath("//div[contains(@class,'ui dropdown link item') and contains(.,'Manage Requests')]");
        private By ReceivedRequestsOption => By.XPath("//div[contains(@class,'menu')]//a[normalize-space()='Received Requests']");
        private By ReceivedRequestsContainer => By.XPath("//*[@id='received-request-section']/div[2]");
        private By ReceivedRequestsHeading => By.XPath("//*[@id='received-request-section']/div[2]/h2");
        private By EmptyStateMessage => By.XPath("//*[@id='received-request-section']/div[2]/h3");

        public void OpenManageRequestsDropdown()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(ManageRequestsTab)).Click();
        }

        public void ClickReceivedRequests()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(ReceivedRequestsOption)).Click();
        }

        public void GoToReceivedRequests()
        {
            OpenManageRequestsDropdown();
            ClickReceivedRequests();
            WaitForReceivedRequestsSection();
        }

        public void WaitForReceivedRequestsSection()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(ReceivedRequestsContainer));
            wait.Until(ExpectedConditions.ElementIsVisible(ReceivedRequestsHeading));
            wait.Until(ExpectedConditions.ElementIsVisible(EmptyStateMessage));
        }

        public string GetReceivedRequestsHeading()
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(ReceivedRequestsHeading)).Text.Trim();
        }

        public string GetEmptyStateMessage()
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(EmptyStateMessage)).Text.Trim();
        }
    }
}