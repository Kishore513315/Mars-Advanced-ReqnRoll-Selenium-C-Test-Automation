using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace MarsAdvancedReqnRollAutomation.Pages.ManageListings
{
    public class ViewListingPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public ViewListingPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        private By ViewPageTitle => By.XPath("/html/body/div[1]/div[2]/div/div[2]/div[1]/div[1]/div[2]/h1/span");

        public string GetTitle()
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(ViewPageTitle)).Text.Trim();
        }
    }
}