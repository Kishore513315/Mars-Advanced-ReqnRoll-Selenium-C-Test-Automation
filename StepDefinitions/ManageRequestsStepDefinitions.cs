using MarsAdvancedReqnRollAutomation.Drivers;
using MarsAdvancedReqnRollAutomation.Pages.Components.ManageRequests;
using MarsAdvancedReqnRollAutomation.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace MarsAdvancedReqnRollAutomation.StepDefinitions
{
    [Binding]
    public class ManageRequestsStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly ManageRequestsComponent _manageRequestsComponent;

        public ManageRequestsStepDefinitions()
        {
            _driver = Driver.GetDriver();
            _manageRequestsComponent = new ManageRequestsComponent(_driver, 10);
        }

        [When(@"I navigate to Received Requests using test data ""([^""]*)""")]
        public void WhenINavigateToReceivedRequestsUsingTestData(string path)
        {
            _manageRequestsComponent.GoToReceivedRequests();
        }

        [Then(@"I should see the Received Requests heading and empty message from test data ""([^""]*)""")]
        public void ThenIShouldSeeTheReceivedRequestsHeadingAndEmptyMessageFromTestData(string path)
        {
            var data = JsonReader.GetData(path);

            string expectedHeading = data["expectedHeading"]!.ToString();
            string expectedMessage = data["expectedMessage"]!.ToString();

            string actualHeading = _manageRequestsComponent.GetReceivedRequestsHeading();
            string actualMessage = _manageRequestsComponent.GetEmptyStateMessage();

            Assert.That(actualHeading, Is.EqualTo(expectedHeading),
                $"Heading mismatch. Expected: {expectedHeading} Actual: {actualHeading}");

            Assert.That(actualMessage, Is.EqualTo(expectedMessage),
                $"Empty state message mismatch. Expected: {expectedMessage} Actual: {actualMessage}");
        }
    }
}
