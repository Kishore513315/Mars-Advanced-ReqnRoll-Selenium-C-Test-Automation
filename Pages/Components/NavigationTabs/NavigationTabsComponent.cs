using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace MarsAdvancedReqnRollAutomation.Pages.Components.NavigationTabs
{
    public class NavigationTabsComponent
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public NavigationTabsComponent(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        private By ManageListingsTab => By.XPath("/html/body/div[1]/div/section[1]/div/a[3]");

        public void GoToManageListings()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(ManageListingsTab)).Click();
        }
    }
}
