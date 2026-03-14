Feature: Profile - Change Password

@TC_Profile_001
Scenario: TC_Profile_001 Change password successfully and cleanup

    Given I login using test data "TestData/Profile/TC_Profile_001.json"

    When I open Change Password popup from the user menu
    And I change password using test data "TestData/Profile/TC_Profile_001.json"
    Then I should see the toast message from test data "TestData/Profile/TC_Profile_001.json"

    When I sign out from the user menu
    Then I should be able to login with NEW password using test data "TestData/Profile/TC_Profile_001.json"

    When I open Change Password popup from the user menu
    And I change password back to original using test data "TestData/Profile/TC_Profile_001.json"
    Then I should see the toast message from test data "TestData/Profile/TC_Profile_001.json"

    When I sign out from the user menu
    Then I should be able to login with ORIGINAL password using test data "TestData/Profile/TC_Profile_001.json"