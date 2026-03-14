using MarsAdvancedReqnRollAutomation.Drivers;
using MarsAdvancedReqnRollAutomation.Pages.Components.Profile;
using MarsAdvancedReqnRollAutomation.Pages.Components.Profile.Certifications;
using MarsAdvancedReqnRollAutomation.Utilities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace MarsAdvancedReqnRollAutomation.StepDefinitions
{
    [Binding]
    public class ProfileCertificationsManagementStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly CertificationListComponent _certList;
        private readonly CertificationFormComponent _certForm;

        public ProfileCertificationsManagementStepDefinitions()
        {
            _driver = Driver.GetDriver();
            _certList = new CertificationListComponent(_driver);
            _certForm = new CertificationFormComponent(_driver);
        }

        private JObject ReadTcData(string path) => JsonReader.GetData(path);

        [When(@"I add a new certification using test data ""([^""]*)""")]
        public void WhenIAddANewCertificationUsingTestData(string path)
        {
            var data = ReadTcData(path);

            _certList.NavigateToCertificationsTab();
            _certList.ClickAddNew();

            _certForm.EnterCertificate(data["certificate"]!.ToString());
            _certForm.EnterCertifiedFrom(data["from"]!.ToString());
            _certForm.SelectYear(data["year"]!.ToString());
            _certForm.ClickAdd();
        }

        [When(@"I delete a certification using test data ""([^""]*)""")]
        public void WhenIDeleteACertificationUsingTestData(string path)
        {
            var data = ReadTcData(path);

            _certList.NavigateToCertificationsTab();
            _certList.DeleteCertificate(data["certificate"]!.ToString());
        }

        [Then(@"I should see the certification toast message from test data ""([^""]*)""")]
        public void ThenIShouldSeeTheCertificationToastMessageFromTestData(string path)
        {
            var data = ReadTcData(path);
            var expectedToast = data["expectedToast"]!.ToString();

            var toast = ToastMessageReader.GetToastMessage(_driver) ?? "";

            Assert.That(toast, Does.Contain(expectedToast),
                $"Toast mismatch. Expected: {expectedToast} Actual: {toast}");
        }
    }
}