using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using MarsAdvancedReqnRollAutomation.Drivers;
using MarsAdvancedReqnRollAutomation.Pages;
using Newtonsoft.Json.Linq;
using Reqnroll;
using System;
using System.Linq;

namespace MarsAdvancedReqnRollAutomation.Support
{
    [Binding]
    public class Hooks
    {
        private static ExtentReports _extent = Utilities.ExtentManager.GetExtent();
        private static ExtentTest? _feature;
        private static ExtentTest? _scenario;

        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;

        // ✅ Central base URL (keep consistent everywhere)
        private const string BaseUrl = "http://localhost:5003/";

        public Hooks(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _extent = Utilities.ExtentManager.GetExtent();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext feature)
        {
            _feature = _extent.CreateTest<Feature>(feature.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _scenario = _feature!.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);

            Driver.InitializeDriver();

            // ✅ NEW: Always open the AUT at the start of every scenario
            var driver = Driver.GetDriver();
            driver.Navigate().GoToUrl(BaseUrl);

            var scenarioTags = _scenarioContext.ScenarioInfo.Tags;
            var featureTags = _featureContext.FeatureInfo.Tags;

            bool hasLoginTag =
                scenarioTags.Contains("login") ||
                featureTags.Contains("login");

            if (hasLoginTag)
            {
                PerformLogin();
            }
        }

        private void PerformLogin()
        {
            var driver = Driver.GetDriver();
            var loginPage = new LoginPage(driver);

            JObject data = Utilities.JsonReader.GetData("TestData/SignInData.json");
            var user = data["validUser"]
                       ?? throw new Exception("validUser not found in SignInData.json");

            // Your LoginPage navigates + clicks Sign In itself, which is fine.
            loginPage.GoToLoginPage();
            loginPage.EnterCredentials(
                user["Email"]!.ToString(),
                user["Password"]!.ToString()
            );
            loginPage.ClickLogin();

            if (!loginPage.IsDashboardVisible())
                throw new Exception("Login failed — dashboard not visible.");
        }

        [AfterStep]
        public void AfterStep()
        {
            var stepInfo = _scenarioContext.StepContext.StepInfo;

            if (_scenarioContext.TestError == null)
            {
                _scenario!.CreateNode(stepInfo.StepDefinitionType.ToString(), stepInfo.Text);
            }
            else
            {
                string screenshotPath = Driver.TakeScreenshot(_scenarioContext.ScenarioInfo.Title);

                _scenario!.CreateNode(stepInfo.StepDefinitionType.ToString(), stepInfo.Text)
                          .Fail(_scenarioContext.TestError.Message)
                          .AddScreenCaptureFromPath(screenshotPath);
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Driver.QuitDriver();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extent.Flush();
        }
    }
}

