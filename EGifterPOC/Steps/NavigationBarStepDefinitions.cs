using EGifterPOC.Drivers;
using EGifterPOC.Drivers.PageObjects;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace EGifterPOC.Steps
{
    [Binding]
    public sealed class NavigationBarStepDefinitions
    {

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        private readonly NavigationBarWidget _navigationBarWidget;

        public NavigationBarStepDefinitions(NavigationBarWidget navigationBarWidget)
        {
            _navigationBarWidget = navigationBarWidget;
        }

        [StepDefinition(@"I click ""(.*)"" in the Navigation bar")]
        public void IClickInTheNavigationBar(NavigationBarWidget.Tab tab)
        {
            _navigationBarWidget.ClickTab(tab);
        }
    }
}
