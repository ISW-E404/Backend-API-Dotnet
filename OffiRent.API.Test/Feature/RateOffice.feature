Feature: RateOffice
	As a user 
	I want to rate an office
	In order to generate a score 

@mytag
Scenario: User rate an office
	Given the user wants to rate an office 
	When the user clicks on My Reservations page
	And it goes to the past reservations section
	And select the reservation in which the user want to rate the office 
	And  if it's the first time that the user enters to this reservation
	Then the system will show a screen where the user can rate the office in scales between 1 to 5 


Scenario: User want to change the rating of an office
	Given the user wants to change the rating of an office
	When the user clicks on the My Reservations page
	And it goes to the past reservation section
	And select the reservation in which the user want to change the rating of the office
	Then the system will show a screen where the details of your past reservation appear
	And in the score section the user can change the rating of the office

Scenario: User want to change the rating of an office for the second time
	Given the user wants to change the rating of an office for the second time
	When the user clicks on the My Reservations page
	And it goes to the past reservation section
	And select the reservation in which the user want to change the rating of the office
	And it goes to the score section to change the rating
	Then the system will show an error message 