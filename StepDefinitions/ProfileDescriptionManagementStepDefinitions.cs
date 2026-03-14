using MarsAdvancedReqnRollAutomation.Drivers;
using MarsAdvancedReqnRollAutomation.Pages.Components.Profile;
using MarsAdvancedReqnRollAutomation.Pages.Components.Profile.Decscription;
using MarsAdvancedReqnRollAutomation.Utilities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace MarsAdvancedReqnRollAutomation.StepDefinitions
{
    [Binding]
    public class ProfileDescriptionManagementStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly ProfileDescriptionComponent _descriptionComponent;

        public ProfileDescriptionManagementStepDefinitions()
        {
            _driver = Driver.GetDriver();
            _descriptionComponent = new ProfileDescriptionComponent(_driver, 10);
        }

        private JObject ReadTcData(string path) => JsonReader.GetData(path);

        [When(@"I update profile description using test data ""([^""]*)""")]
        public void WhenIUpdateProfileDescriptionUsingTestData(string path)
        {
            var data = ReadTcData(path);

            _descriptionComponent.ClickEdit();
            _descriptionComponent.EnterDescription(data["descriptionText"]!.ToString());
            _descriptionComponent.ClickSave();
        }

        [Then(@"I should see the profile description toast message from test data ""([^""]*)""")]
        public void ThenIShouldSeeTheProfileDescriptionToastMessageFromTestData(string path)
        {
            var data = ReadTcData(path);
            var expectedToast = data["expectedToast"]!.ToString();

            var toast = ToastMessageReader.GetToastMessage(_driver) ?? "";

            Assert.That(toast, Does.Contain(expectedToast),
                $"Toast mismatch. Expected: {expectedToast} Actual: {toast}");
        }
    }
}
