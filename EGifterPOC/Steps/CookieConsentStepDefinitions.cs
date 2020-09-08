using EGifterPOC.Drivers.PageObjects;
using TechTalk.SpecFlow;

namespace EGifterPOC.Steps
{
    [Binding]
    public sealed class CookieConsentStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        private readonly CookieConsentWidget _cookieConsentWidget;

        public CookieConsentStepDefinitions(CookieConsentWidget cookieConsentWidget)
        {
            _cookieConsentWidget = cookieConsentWidget;
        }

        [Given(@"I dismiss cookie consent")]
        public void GivenIDismissCookieConsent()
        {
            _cookieConsentWidget.DismissMessage();
        }
    }
}