using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MarsAdvancedReqnRollAutomation.Pages.Components.TopMenu
{
    public class ChangePasswordModalComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public ChangePasswordModalComponent(IWebDriver driver, int timeoutSeconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        }

        private By ActiveModal => By.CssSelector("div.ui.mini.modal.transition.visible.active");

        private By CurrentPasswordInput => By.Name("oldPassword");
        private By NewPasswordInput => By.Name("newPassword");
        private By ConfirmPasswordInput => By.Name("confirmPassword");

        private By SaveButton => By.XPath("//div[contains(@class,'modal') and contains(@class,'active')]//button[normalize-space()='Save']");

        public void WaitForModal()
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(ActiveModal));
        }

        public void ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            WaitForModal();

            _wait.Until(ExpectedConditions.ElementIsVisible(CurrentPasswordInput)).Clear();
            _driver.FindElement(CurrentPasswordInput).SendKeys(currentPassword);

            _driver.FindElement(NewPasswordInput).Clear();
            _driver.FindElement(NewPasswordInput).SendKeys(newPassword);

            _driver.FindElement(ConfirmPasswordInput).Clear();
            _driver.FindElement(ConfirmPasswordInput).SendKeys(confirmPassword);

            _driver.FindElement(SaveButton).Click();
        }
    }
}
