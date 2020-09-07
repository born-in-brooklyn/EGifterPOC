using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace EGifterPOC.Drivers.PageObjects
{
    [Binding]
    public class BuyGiftCardsPageObject
    {
        private readonly Configuration _configuration;
        private readonly ActAndWaitUntilAssertion _actAndWaitUntilAssertion;

        public BuyGiftCardsPageObject(RemoteWebDriver webDriver, Configuration configuration, ActAndWaitUntilAssertion actAndWaitUntilAssertion)
        {
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
            _configuration = configuration;
            CategoriesMenuWidget = new CategoriesMenuWidget(_actAndWaitUntilAssertion);
            ProductSearchWidget = new ProductSearchWidget(_actAndWaitUntilAssertion);
            ProductGridWidget = new ProductGridWidget(_actAndWaitUntilAssertion);
            ProductDetailsWidget = new ProductDetailsWidget(_actAndWaitUntilAssertion);
        }

        public CategoriesMenuWidget CategoriesMenuWidget { get; }
        public ProductSearchWidget ProductSearchWidget { get; }
        public ProductGridWidget ProductGridWidget { get; }
        public ProductDetailsWidget ProductDetailsWidget { get; }

        public void Load()
        {
            _actAndWaitUntilAssertion.GoToUrlAndWaitForElement(
                new Uri(_configuration.HomePageUri, "/giftcards"),
                NavigationBarWidget.HomeTabXPathSelector,
                "Home page failed to load");
        }
    }
}
