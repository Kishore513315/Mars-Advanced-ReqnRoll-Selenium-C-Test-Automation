using MarsAdvancedReqnRollAutomation.Drivers;
using MarsAdvancedReqnRollAutomation.Pages.Components.Profile;
using MarsAdvancedReqnRollAutomation.Utilities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace MarsAdvancedReqnRollAutomation.StepDefinitions
{
    [Binding]
    public class ProfileEducationManagementStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly EducationListComponent _educationList;
        private readonly EducationFormComponent _educationForm;

        public ProfileEducationManagementStepDefinitions()
        {
            _driver = Driver.GetDriver();
            _educationList = new EducationListComponent(_driver);
            _educationForm = new EducationFormComponent(_driver);
        }

        private JObject ReadTcData(string path) => JsonReader.GetData(path);

        [When(@"I add a new education using test data ""([^""]*)""")]
        public void WhenIAddANewEducationUsingTestData(string path)
        {
            var data = ReadTcData(path);

            _educationList.NavigateToEducationTab();
            _educationList.ClickAddNew();

            _educationForm.EnterUniversity(data["university"]!.ToString());
            _educationForm.SelectCountry(data["country"]!.ToString());
            _educationForm.SelectTitle(data["title"]!.ToString());
            _educationForm.EnterDegree(data["degree"]!.ToString());
            _educationForm.SelectYear(data["year"]!.ToString());

            _educationForm.ClickAdd();
        }

        [When(@"I delete the education record")]
        public void WhenIDeleteTheEducationRecord()
        {
            _educationList.NavigateToEducationTab();
            _educationList.ClickDelete();
        }

        [Then(@"I should see the education toast message from test data ""([^""]*)""")]
        public void ThenIShouldSeeTheEducationToastMessageFromTestData(string path)
        {
            var data = ReadTcData(path);
            var expectedToast = data["expectedToast"]!.ToString();

            var toast = ToastMessageReader.GetToastMessage(_driver) ?? "";

            Assert.That(toast, Does.Contain(expectedToast),
                $"Toast mismatch. Expected: {expectedToast} Actual: {toast}");
        }
    }
}