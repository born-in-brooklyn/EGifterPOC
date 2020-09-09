using System;
using FluentAssertions;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace EGifterPOC.Drivers.PageObjects
{
    [Binding]
    public class ShoppingCartPageObject
    {
        private readonly ActAndWaitUntilAssertion _actAndWaitUntilAssertion;
        private readonly Configuration _configuration;

        public ShoppingCartPageObject(
            RemoteWebDriver webDriver, 
            Configuration configuration,
            ActAndWaitUntilAssertion actAndWaitUntilAssertion,
            TotalSectionWidget totalSectionWidget,
            LineItemsWidget lineItemsWidget)
        {
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
            _configuration = configuration;
            TotalSectionWidget = totalSectionWidget;
            LineItemsWidget = lineItemsWidget;
        }

        public TotalSectionWidget TotalSectionWidget { get; }
        public LineItemsWidget LineItemsWidget { get; }

        public void Load()
        {
            _actAndWaitUntilAssertion.GoToUrlAndWaitForElement(
                new Uri(_configuration.HomePageUri, "/cart"),
                NavigationBarWidget.HomeTabXPathSelector,
                "Shopping cart page failed to load");
        }

        public void ProceedToCheckout()
        {
            _actAndWaitUntilAssertion.ClickAndWaitForElement(
                "//span[@key='Cart_ProceedToCheckoutButton']",
                "//span[@key='Checkout_PageTitle']",
                "checkout page failed to load");
        }
    }
}