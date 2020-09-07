using EGifterPOC.Drivers;
using EGifterPOC.Drivers.PageObjects;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace EGifterPOC.Steps
{
    [Binding]
    public sealed class HomePageStepDefinitions
    {

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        private readonly HomePageObject _homePageObject;

        public HomePageStepDefinitions(HomePageObject homePageObject)
        {
            _homePageObject = homePageObject;
        }

        [StepDefinition("I load the home page")]
        public void ILoadTheHomePage()
        {
            _homePageObject.Load();
        }
    }
}
