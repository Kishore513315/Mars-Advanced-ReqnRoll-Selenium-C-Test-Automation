using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MarsAdvancedReqnRollAutomation.Pages.Components.Profile.Decscription
{
    public class ProfileDescriptionComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public ProfileDescriptionComponent(IWebDriver driver, int timeoutSeconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        }

        private By DescriptionPencil =>
            By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[3]/div/div/div/h3/span/i");

        private By DescriptionTextbox =>
            By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[3]/div/div/form/div/div/div[2]/div[1]/textarea");

        private By SaveButton =>
            By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[3]/div/div/form/div/div/div[2]/button");

        public void ClickEdit()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(DescriptionPencil)).Click();
        }

        public void EnterDescription(string text)
        {
            var textarea = _wait.Until(ExpectedConditions.ElementIsVisible(DescriptionTextbox));
            textarea.Clear();
            textarea.SendKeys(text);
        }

        public void ClickSave()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(SaveButton)).Click();
        }
    }
}
