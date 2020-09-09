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
        private readonly RemoteWebDriver _webDriver;

        public CheckoutPageObject(RemoteWebDriver webDriver, Configuration configuration,
            ActAndWaitUntilAssertion actAndWaitUntilAssertion)
        {
            _webDriver = webDriver;
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
            _configuration = configuration;
            CheckoutSummaryItemsWidget = new CheckoutSummaryItemsWidget(webDriver);
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
                "//span[@key='Continue as Guest']",
                "//span[@key='PaymentMethodTiles_Header']",
                "payment methods failed to load");
        }

        public bool AmountDueMatches(string amountDue)
        {
            try
            {
                _webDriver.FindElementByXPath($"//section[contains(@class, 'checkoutAmountComponent') and //*[normalize-space(text())='{amountDue}']]");
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}