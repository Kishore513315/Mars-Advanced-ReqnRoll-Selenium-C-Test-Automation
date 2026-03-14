using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MarsAdvancedReqnRollAutomation.Pages.Components.TopMenu
{
    public class UserMenuComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public UserMenuComponent(IWebDriver driver, int timeoutSeconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        }

        private By UserDropdown => By.CssSelector("span.item.ui.dropdown.link");

        private By ChangePasswordLink => By.XPath("//a[normalize-space()='Change Password']");
        private By SignOutButton => By.XPath("//button[normalize-space()='Sign Out']");

        public void OpenUserMenu()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(UserDropdown)).Click();
        }

        public void ClickChangePassword()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(ChangePasswordLink)).Click();
        }

        public void ClickSignOut()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(SignOutButton)).Click();
        }
    }
}
