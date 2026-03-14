Feature: Profile Certifications Management

@login
Scenario: TC_Profile_Cert_004 Add New Certification
    When I add a new certification using test data "TestData/Profile/Certifications/TC_Profile_Cert_004.json"
    Then I should see the certification toast message from test data "TestData/Profile/Certifications/TC_Profile_Cert_004.json"


@login
Scenario: TC_Profile_Cert_005 Delete Certification
    When I delete a certification using test data "TestData/Profile/Certifications/TC_Profile_Cert_005.json"
    Then I should see the certification toast message from test data "TestData/Profile/Certifications/TC_Profile_Cert_005.json"
