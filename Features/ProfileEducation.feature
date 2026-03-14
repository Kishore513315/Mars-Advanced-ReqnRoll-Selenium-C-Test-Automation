Feature: Profile Education Management

@login
Scenario: TC_Profile_Edu_002 Add new Education
    When I add a new education using test data "TestData/Profile/Education/TC_Profile_Edu_002.json"
    Then I should see the education toast message from test data "TestData/Profile/Education/TC_Profile_Edu_002.json"


@login
Scenario: TC_Profile_Edu_003 Delete Education
    When I delete the education record
    Then I should see the education toast message from test data "TestData/Profile/Education/TC_Profile_Edu_003.json"
