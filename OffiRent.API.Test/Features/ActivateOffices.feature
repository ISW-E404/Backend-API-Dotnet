Feature: ActivateOffices
	As a offi-provider premium
	I want to be activate a deactivated office
	In order to publish it again in the app

@mytag
Scenario: Offi-provider premium activates an Office
	Given offi-provider has Premium Account
	And offi-provider is in the deactivated office window
	When offi-provider clicks in Activate product 
	Then the system change the office status to activated

Scenario: Offi-provider has not a Premium Account
	Given offi-provider has not a Premium Account
	And offi-provider is in the deactivated office window
	When offi-provider clicks in Activate product 
	Then the system shows the message This Account is not premium
