Feature: CreateOffice
	As a provider
	I want to create an office publication
	So the interested people could later visualize it

Scenario: User creates office successfully
	Given the provider specifies properties for his office
	And his office meets all the requirements
	When the user sends the data to the system
	Then the system will save the office successfully

Scenario: User attempts to create office without specifying a price
	Given the provider specifies properties for his office
	And the provider doesn't specify the price for the office
	When the user sends the data without price to the system
	Then the system will return an error response message, asking to specify a price

Scenario: User attempts to create office without specifying a district
	Given the provider specifies properties for his office
	And the provider doesn't specify the district of the office
	When the user sends the data to the system without district
	Then the system will return an error response message, asking to specify a district