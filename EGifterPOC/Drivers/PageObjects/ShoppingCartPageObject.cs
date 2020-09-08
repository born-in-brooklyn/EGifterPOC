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

        public ShoppingCartPageObject(RemoteWebDriver webDriver, Configuration configuration,
            ActAndWaitUntilAssertion actAndWaitUntilAssertion)
        {
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
            _configuration = configuration;
            TotalSectionWidget = new TotalSectionWidget(webDriver);
            LineItemsWidget = new LineItemsWidget(webDriver, actAndWaitUntilAssertion);
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
    }
}