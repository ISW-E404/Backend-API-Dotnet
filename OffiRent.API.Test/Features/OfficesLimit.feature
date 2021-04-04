Feature: OfficesLimit

Scenario: Non-Premium user attempts to create more than one post
	Given the user has specified data for a new office post
	And the user already has an active office post
	When the user sends this resource to the system
	Then the system will return an error message

Scenario: Premium user successfully creates more than one post
	Given the user has specified data for a new office
	And the user has less than 15 active office posts
	When the user sends this resource to the system
	Then the system will successfuly save the new post

Scenario: Premium tries to create more than 15 posts
	Given the user has specified data for a new office
	And the user has more than 14 active office posts
	When the user sends this resource to the system
	Then the system will return an premium error message