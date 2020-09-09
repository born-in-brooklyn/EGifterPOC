using System.Linq;
using EGifterPOC.Drivers.PageObjects;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace EGifterPOC.Steps
{
    [Binding]
    public sealed class CheckoutStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        private readonly CheckoutPageObject _checkoutPageObject;

        public CheckoutStepDefinitions(CheckoutPageObject checkoutPageObject)
        {
            _checkoutPageObject = checkoutPageObject;
        }

        [Then(@"I should see Amount Due (.*)")]
        public void ThenIShouldSeeAmountDue(string expectedItemTotal)
        {
            _checkoutPageObject.AmountDueMatches(expectedItemTotal).Should().BeTrue();
        }

        [Then(@"I should see the following items in the checkout summary:")]
        public void ThenIShouldSeeTheFollowingItemsInTheCheckoutSummary(Table table)
        {
            var shoppingCartItems = table.CreateSet<CheckoutSummaryItem>().ToArray();
            shoppingCartItems
                .Select((sci, i) =>
                {
                    return _checkoutPageObject.CheckoutSummaryItemsWidget.MatchesItemExpectations(i, sci);
                })
                .All(v => v==true).Should().BeTrue();
        }

        [Then(@"I click Continue as Guest")]
        public void ThenIClickContinueAsGuest()
        {
            _checkoutPageObject.ContinueAsGuest();
        }
    }
}