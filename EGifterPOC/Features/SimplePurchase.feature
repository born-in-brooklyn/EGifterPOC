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
	Then I should see The following items in my shopping cart:
	| Name                    | To     | Value | Quantity | Total |
	| AMC Theatres eGift Card | Myself | $50   | 1        | $50   |
	And I should see an item total of 1 item
	And I should see a total amount of $50.00