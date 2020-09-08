using System;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace EGifterPOC.Drivers.PageObjects
{
    [Binding]
    public class BuyGiftCardsPageObject
    {
        private readonly ActAndWaitUntilAssertion _actAndWaitUntilAssertion;
        private readonly Configuration _configuration;

        public BuyGiftCardsPageObject(RemoteWebDriver webDriver, Configuration configuration,
            ActAndWaitUntilAssertion actAndWaitUntilAssertion)
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
                "Buy Gift Cards page failed to load");
        }
    }
}