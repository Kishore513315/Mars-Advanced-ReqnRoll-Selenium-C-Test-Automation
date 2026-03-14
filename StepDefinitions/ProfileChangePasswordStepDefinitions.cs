using MarsAdvancedReqnRollAutomation.Drivers;
using MarsAdvancedReqnRollAutomation.Pages;
using MarsAdvancedReqnRollAutomation.Pages.Components.TopMenu;       
using MarsAdvancedReqnRollAutomation.Utilities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using System;

namespace MarsAdvancedReqnRollAutomation.StepDefinitions
{
    [Binding]
    public class ProfileChangePasswordStepDefinitions
    {
        private readonly IWebDriver _driver;

        public ProfileChangePasswordStepDefinitions()
        {
            _driver = Driver.GetDriver();
        }

        private JObject ReadTcData(string path) => JsonReader.GetData(path);

        private (string Email, string CurrentPwd, string NewPwd, string ExpectedToast) GetTcFields(string path)
        {
            var data = ReadTcData(path);

            var email = data["email"]?.ToString() ?? "";
            var currentPwd = data["currentPassword"]?.ToString() ?? "";
            var newPwd = data["newPassword"]?.ToString() ?? "";
            var expectedToast = data["expectedToast"]?.ToString() ?? "Password Changed Successfully";

            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("email is missing in TC JSON.");

            if (string.IsNullOrWhiteSpace(currentPwd))
                throw new Exception("currentPassword is missing in TC JSON.");

            if (string.IsNullOrWhiteSpace(newPwd))
                throw new Exception("newPassword is missing in TC JSON.");

            return (email, currentPwd, newPwd, expectedToast);
        }

        [Given(@"I login using test data ""([^""]*)""")]
        public void GivenILoginUsingTestData(string path)
        {
            var (email, currentPwd, _, _) = GetTcFields(path);

            var loginPage = new LoginPage(_driver);
            loginPage.GoToLoginPage();
            loginPage.EnterCredentials(email, currentPwd);
            loginPage.ClickLogin();

            Assert.That(loginPage.IsDashboardVisible(), Is.True, "Login failed with ORIGINAL password (from TC JSON).");
        }

        [When(@"I open Change Password popup from the user menu")]
        public void WhenIOpenChangePasswordPopupFromTheUserMenu()
        {
            var menu = new UserMenuComponent(_driver, 10);
            menu.OpenUserMenu();
            menu.ClickChangePassword();
        }

        [When(@"I change password using test data ""([^""]*)""")]
        public void WhenIChangePasswordUsingTestData(string path)
        {
            var (_, currentPwd, newPwd, _) = GetTcFields(path);

            var modal = new ChangePasswordModalComponent(_driver, 10);
            modal.ChangePassword(currentPwd, newPwd, newPwd);
        }

        [Then(@"I should see the toast message from test data ""([^""]*)""")]
        public void ThenIShouldSeeTheToastMessageFromTestData(string path)
        {
            var (_, _, _, expectedToast) = GetTcFields(path);

            var toast = ToastMessageReader.GetToastMessage(_driver) ?? "";
            Assert.That(toast, Does.Contain(expectedToast),
                $"Toast mismatch. Expected to contain: '{expectedToast}'. Actual: '{toast}'");
        }

        [When(@"I sign out from the user menu")]
        public void WhenISignOutFromTheUserMenu()
        {
            var menu = new UserMenuComponent(_driver, 10);
            menu.OpenUserMenu();
            menu.ClickSignOut();
        }

        [Then(@"I should be able to login with NEW password using test data ""([^""]*)""")]
        public void ThenIShouldBeAbleToLoginWithNewPasswordUsingTestData(string path)
        {
            var (email, _, newPwd, _) = GetTcFields(path);

            var loginPage = new LoginPage(_driver);
            loginPage.GoToLoginPage();
            loginPage.EnterCredentials(email, newPwd);
            loginPage.ClickLogin();

            Assert.That(loginPage.IsDashboardVisible(), Is.True, "Login with NEW password failed.");
        }

        [When(@"I change password back to original using test data ""([^""]*)""")]
        public void WhenIChangePasswordBackToOriginalUsingTestData(string path)
        {
            var (_, currentPwd, newPwd, _) = GetTcFields(path);

            var modal = new ChangePasswordModalComponent(_driver, 10);
            modal.ChangePassword(newPwd, currentPwd, currentPwd);
        }

        [Then(@"I should be able to login with ORIGINAL password using test data ""([^""]*)""")]
        public void ThenIShouldBeAbleToLoginWithOriginalPasswordUsingTestData(string path)
        {
            var (email, currentPwd, _, _) = GetTcFields(path);

            var loginPage = new LoginPage(_driver);
            loginPage.GoToLoginPage();
            loginPage.EnterCredentials(email, currentPwd);
            loginPage.ClickLogin();

            Assert.That(loginPage.IsDashboardVisible(), Is.True, "Login with ORIGINAL password failed after cleanup.");
        }
    }
}