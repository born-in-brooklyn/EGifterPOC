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
	And I click the 'Buy for Myself' button
	And I click "Buy Gift Cards" in the Navigation bar
	And I select the "adidas" card from the catalog
	And I click the 'Buy for Myself' button
	Then I should see the following items in my shopping cart:
	| Name                    | To     | Value | Quantity | Total |
	| AMC Theatres eGift Card | Myself | $50   | 1        | $50   |
	| adidas eGift Card       | Myself | $25   | 1        | $25   |
	And I should see an item total of 2 items
	And I should see a total amount of $75.00
	And I proceed to checkout
	And I should see the following items in the checkout summary:
	| Name                    | Quantity | Amount |
	| AMC Theatres eGift Card | 1        | $50    |
	| adidas eGift Card       | 1        | $25    |
	And I should see Amount Due $75.00
