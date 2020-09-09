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

        public BuyGiftCardsPageObject(Configuration configuration,
            ActAndWaitUntilAssertion actAndWaitUntilAssertion,
            CategoriesMenuWidget categoriesMenuWidget,
            ProductSearchWidget productSearchWidget,
            ProductGridWidget productGridWidget,
            ProductDetailsWidget productDetailsWidget
            )
        {
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
            _configuration = configuration;
            CategoriesMenuWidget = categoriesMenuWidget;
            ProductSearchWidget = productSearchWidget;
            ProductGridWidget = productGridWidget;
            ProductDetailsWidget = productDetailsWidget;
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