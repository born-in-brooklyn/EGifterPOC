using System;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace EGifterPOC.Drivers.PageObjects
{
    [Binding]
    public class CheckoutPageObject
    {
        private readonly ActAndWaitUntilAssertion _actAndWaitUntilAssertion;
        private readonly Configuration _configuration;

        public CheckoutPageObject(RemoteWebDriver webDriver, Configuration configuration,
            ActAndWaitUntilAssertion actAndWaitUntilAssertion)
        {
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
            _configuration = configuration;
            CheckoutSummaryItemsWidget = new CheckoutSummaryItemsWidget(actAndWaitUntilAssertion);
        }

        public CheckoutSummaryItemsWidget CheckoutSummaryItemsWidget { get; }

        public void Load()
        {
            _actAndWaitUntilAssertion.GoToUrlAndWaitForElement(
                new Uri(_configuration.HomePageUri, "/checkout"),
                NavigationBarWidget.HomeTabXPathSelector,
                "Checkout page failed to load");
        }

        public void ContinueAsGuest()
        {
            _actAndWaitUntilAssertion.ClickAndWaitForElement(
                "//span[@token='Continue as Guest']",
                "//span[@token='PaymentMethodTiles_Header']",
                "payment methods failed to load");
        }

        public bool AmountDueMatches(string amountDue)
        {
            try
            {
                _actAndWaitUntilAssertion.WaitForElementWithRetry($"//section[contains(@class, 'checkoutAmountComponent') and //*[normalize-space(text())='{amountDue}']]");
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}