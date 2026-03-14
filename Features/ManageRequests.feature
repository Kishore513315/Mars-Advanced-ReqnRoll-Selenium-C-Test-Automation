Feature: Manage Requests

@login
Scenario: TC_MR_002 Check for any Received Requests
    When I navigate to Received Requests using test data "TestData/ManageRequests/TC_MR_002.json"
    Then I should see the Received Requests heading and empty message from test data "TestData/ManageRequests/TC_MR_002.json"
