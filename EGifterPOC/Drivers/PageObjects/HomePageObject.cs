using TechTalk.SpecFlow;

namespace EGifterPOC.Drivers.PageObjects
{
    [Binding]
    public class HomePageObject
    {
        private readonly ActAndWaitUntilAssertion _actAndWaitUntilAssertion;
        private readonly Configuration _configuration;

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