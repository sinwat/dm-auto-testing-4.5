Feature: Login
	In order to use dm.am website
	I want to log in as an existing user

Scenario: Open the login lightbox
	Given I am on home page
	When I click login button
	Then the login form should appear on the screen
