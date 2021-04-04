Feature: ActivateReservations
	As a offi-user
	I want to activate a reservation
	In order to use the service I've paid

@mytag
Scenario: Offi-user activates a Reservation
	Given offi-user has a Reservation
	And offi-user is in the deactivated reservation window
	When offi-user clicks in Activate reservation 
	Then the system change the reservation status to activated
