using System.Linq;
using EGifterPOC.Drivers.PageObjects;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace EGifterPOC.Steps
{
    [Binding]
    public sealed class ShoppingCartStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        private readonly ShoppingCartPageObject _shoppingCartPageObject;

        public ShoppingCartStepDefinitions(ShoppingCartPageObject shoppingCartPageObject)
        {
            _shoppingCartPageObject = shoppingCartPageObject;
        }

        [Then(@"I should see The following items in my shopping cart:")]
        public void ThenIShouldSeeTheFollowingItemsInMyShoppingCart(Table table)
        {
            var shoppingCartItems = table.CreateSet<ShoppingCartItem>().ToArray();
            shoppingCartItems
                .Select((sci, i) =>
                {
                    return _shoppingCartPageObject.LineItemsWidget.MatchesItemExpectations(i, sci);
                })
                .All(v => v==true).Should().BeTrue();
        }

        [Then(@"I should see an item total of (.*) items?")]
        public void ThenIShouldSeeAnItemTotalOfItems(int expectedItemTotal)
        {
            _shoppingCartPageObject.TotalSectionWidget.DoesItemTotalMatch(expectedItemTotal).Should().BeTrue();
        }

        [Then(@"I should see a total amount of (.*)")]
        public void ThenIShouldSeeAnItemTotalOfItems(string expectedTotalAmount)
        {
            _shoppingCartPageObject.TotalSectionWidget.DoesTotalAmountMatch(expectedTotalAmount).Should().BeTrue();
        }

        [Then(@"I proceed to checkout")]
        public void ThenIProceedToCheckout()
        {
            _shoppingCartPageObject.ProceedToCheckout();
        }


    }
}