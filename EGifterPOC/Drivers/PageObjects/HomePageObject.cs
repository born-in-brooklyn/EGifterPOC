using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace EGifterPOC.Drivers.PageObjects
{
    [Binding]
    public class HomePageObject
    {
        private readonly Configuration _configuration;
        private readonly ActAndWaitUntilAssertion _actAndWaitUntilAssertion;

        public HomePageObject(Configuration configuration, ActAndWaitUntilAssertion actAndWaitUntilAssertion)
        {
            _configuration = configuration;
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
        }

        public void Load()
        {
            _actAndWaitUntilAssertion.GoToUrlAndWaitForElement(
                _configuration.HomePageUri, 
                NavigationBarWidget.HomeTabXPathSelector,
                "Home page failed to load");
        }
    }
}
