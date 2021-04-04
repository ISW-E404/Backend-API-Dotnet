Feature: AccountDetails
	As a user 
	I want to visualize the details of my account
	In order to verify my personal information

@mytag
Scenario: User want to verify the authenticity of his account
	Given a verified user
	And the user want to validate his personal information
	When the user clicks on his profile icon
	Then the system will show his personal information