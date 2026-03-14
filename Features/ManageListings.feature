Feature: Manage Listings

    @login
    Scenario:TC_ML_001 Edit listing description
    Given I am on Manage Listings page
    When I edit listing description using test data "TestData/ManageListings/TC_ML_001.json"
    Then I should see the edit listing toast message from test data "TestData/ManageListings/TC_ML_001.json"

    @login
  Scenario: TC_ML_002 View listing and verify title matches
    Given I am on Manage Listings page
    When I view listing starting with "Performance"
    Then the view page title should start with "Performance"

    @login
  Scenario: TC_ML_003 Cancel delete by clicking No
    Given I am on Manage Listings page
    When I attempt to delete listing starting with "Performance" and click "No"
    Then listing starting with "Performance" should still be present

    @login
  Scenario: TC_ML_004 Delete by clicking Yes
    Given I am on Manage Listings page
    When I attempt to delete listing starting with "Performance" and click "Yes"
    Then I should see toast message contains "has been deleted"

    @login
  Scenario: TC_ML_005 Toggle Active to Inactive
    Given I am on Manage Listings page
    When I toggle active status for listing starting with "Performance"
    Then I should see toast message contains "deactivated"
