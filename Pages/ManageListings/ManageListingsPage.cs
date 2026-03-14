using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsAdvancedReqnRollAutomation.Pages.ManageListings
{
    public class ManageListingsPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public ManageListingsPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        private By ListingSection => By.Id("listing-management-section");

        private By Rows => By.XPath("//*[@id='listing-management-section']//table/tbody/tr");

        private By PopupYes => By.XPath("/html/body/div[2]/div/div[3]/button[2]");
        private By PopupNo => By.XPath("/html/body/div[2]/div/div[3]/button[1]");

        public void WaitForPageReady()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(ListingSection));
            wait.Until(d => d.FindElements(Rows).Count > 0);
        }

        public IWebElement FindRowByTitleStartsWith(string prefix)
        {
            var rows = driver.FindElements(Rows);

            foreach (var row in rows)
            {
                var title = row.FindElement(By.XPath(".//td[3]")).Text.Trim();

                if (title.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    return row;
            }

            throw new NoSuchElementException($"No listing row found with title starting with '{prefix}'.");
        }

        public void ClickView(string titlePrefix)
        {
            var row = FindRowByTitleStartsWith(titlePrefix);
            ScrollActionsIntoView(row);
            row.FindElement(By.XPath(".//td[8]//button[1]")).Click();
        }

        public void ClickEdit(string titlePrefix)
        {
            var row = FindRowByTitleStartsWith(titlePrefix);
            ScrollActionsIntoView(row);
            row.FindElement(By.XPath(".//td[8]//button[2]")).Click();
        }

        public void ClickDelete(string titlePrefix)
        {
            var row = FindRowByTitleStartsWith(titlePrefix);
            ScrollActionsIntoView(row);
            row.FindElement(By.XPath(".//td[8]//button[3]")).Click();
        }

        public void ToggleActive(string titlePrefix)
        {
            var row = FindRowByTitleStartsWith(titlePrefix);
            ScrollActionsIntoView(row);
            row.FindElement(By.XPath(".//td[7]//input")).Click();
        }

        public void ConfirmDeleteYes()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(PopupYes)).Click();
        }

        public void ConfirmDeleteNo()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(PopupNo)).Click();
        }

        public bool IsListingStillPresent(string titlePrefix)
        {
            try
            {
                FindRowByTitleStartsWith(titlePrefix);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ScrollActionsIntoView(IWebElement row)
        {
            var actionsCell = row.FindElement(By.XPath(".//td[8]"));
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].scrollIntoView({block:'center', inline:'center'});",
                actionsCell
            );
        }
    }
}
