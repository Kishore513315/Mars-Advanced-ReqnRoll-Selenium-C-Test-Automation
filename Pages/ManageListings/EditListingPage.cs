using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace MarsAdvancedReqnRollAutomation.Pages.ManageListings
{
    public class EditListingPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public EditListingPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        private By DescriptionTextbox => By.XPath("//textarea[@name='description']");
        private By SaveButton => By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[11]/div/input[1]");

        public void UpdateDescriptionAndSave(string description)
        {
            var descBox = wait.Until(ExpectedConditions.ElementIsVisible(DescriptionTextbox));

            descBox.Clear();
            descBox.SendKeys(description);

            wait.Until(ExpectedConditions.ElementToBeClickable(SaveButton)).Click();
        }
    }
}
