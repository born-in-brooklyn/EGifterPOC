Feature: Simple Purchase
	In order to ensure that the EGifter website is able to affect a purchase
	As an EGifter Customer
	I want to be able to buy a gift

Scenario: Buy a gift for myself as a guest
	Given I load the home page
	And I dismiss cookie consent
	And I click "Buy Gift Cards" in the Navigation bar
	And I select the "Movies & Entertainment" category
	And I search for "AMC Theatres"
	And I select the "AMC Theatres" card from the catalog
	And I select $50 as the value
	When I click the 'Buy for Myself' button