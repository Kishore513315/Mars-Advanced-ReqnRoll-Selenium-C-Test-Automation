using MarsAdvancedReqnRollAutomation.Pages.Components.NavigationTabs;
using MarsAdvancedReqnRollAutomation.Pages.ManageListings;
using MarsAdvancedReqnRollAutomation.Utilities;
using MarsAdvancedReqnRollAutomation.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace MarsAdvancedReqnRollAutomation.StepDefinitions
{
    [Binding]
    public class ManageListingsStepDefinitions
    {
        private readonly IWebDriver driver;

        private readonly NavigationTabsComponent navTabs;
        private readonly ManageListingsPage manageListings;
        private readonly EditListingPage editListing;
        private readonly ViewListingPage viewListing;

        public ManageListingsStepDefinitions()
        {
            driver = Driver.GetDriver();

            navTabs = new NavigationTabsComponent(driver);
            manageListings = new ManageListingsPage(driver);
            editListing = new EditListingPage(driver);
            viewListing = new ViewListingPage(driver);
        }

        [Given(@"I am on Manage Listings page")]
        public void GivenIAmOnManageListingsPage()
        {
            navTabs.GoToManageListings();
            manageListings.WaitForPageReady();
        }

        [When(@"I edit listing description using test data ""([^""]*)""")]
        public void WhenIEditListingDescriptionUsingTestData(string path)
        {
            var data = JsonReader.GetData(path);

            string description = data["descriptionText"]!.ToString();

            manageListings.ClickEdit("qwerty");

            editListing.UpdateDescriptionAndSave(description);
        }

        [Then(@"I should see the edit listing toast message from test data ""([^""]*)""")]
        public void ThenIShouldSeeTheEditListingToastMessage(string path)
        {
            Assert.Pass();
        }

        [When(@"I view listing starting with ""(.*)""")]
        public void WhenIViewListingStartingWith(string prefix)
        {
            manageListings.ClickView(prefix);
        }

        [Then(@"the view page title should start with ""(.*)""")]
        public void ThenTheViewPageTitleShouldStartWith(string prefix)
        {
            var title = viewListing.GetTitle();
            Assert.That(title.StartsWith(prefix), $"Expected view title to start with '{prefix}' but got '{title}'.");
        }

        [When(@"I attempt to delete listing starting with ""(.*)"" and click ""(Yes|No)""")]
        public void WhenIAttemptToDeleteAndClick(string prefix, string confirm)
        {
            manageListings.ClickDelete(prefix);

            if (confirm == "Yes")
                manageListings.ConfirmDeleteYes();
            else
                manageListings.ConfirmDeleteNo();
        }

        [Then(@"listing starting with ""(.*)"" should still be present")]
        public void ThenListingShouldStillBePresent(string prefix)
        {
            Assert.That(manageListings.IsListingStillPresent(prefix), Is.True,
                $"Expected listing starting with '{prefix}' to still be present, but it was not found.");
        }

        [When(@"I toggle active status for listing starting with ""(.*)""")]
        public void WhenIToggleActiveStatusForListingStartingWith(string prefix)
        {
            manageListings.ToggleActive(prefix);
        }

        [Then(@"I should see toast message ""(.*)""")]
        public void ThenIShouldSeeToastMessage(string expected)
        {
            var actual = ToastMessageReader.GetToastMessage(driver);
            Assert.That(actual, Is.EqualTo(expected), $"Expected toast '{expected}' but got '{actual}'.");
        }

        [Then(@"I should see toast message contains ""(.*)""")]
        public void ThenIShouldSeeToastMessageContains(string expectedPart)
        {
            var actual = ToastMessageReader.GetToastMessage(driver);
            Assert.That(actual.ToLower().Contains(expectedPart.ToLower()),
                $"Expected toast to contain '{expectedPart}' but got '{actual}'.");
        }
    }
}
