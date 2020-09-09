using System.Threading;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace EGifterPOC.Drivers.PageObjects
{
    [Binding]
    public class CookieConsentWidget
    {
        private const string CookieConsentXPathSelector =
            "//div[@aria-label='cookieconsent']//a[@aria-label='dismiss cookie message']";
        private const string CookieConsentedXPathSelector =
            "//div[@aria-label='cookieconsent'and contains(@style,'display: none;')]//a[@aria-label='dismiss cookie message']";

        private readonly ActAndWaitUntilAssertion _actAndWaitUntilAssertion;
        public CookieConsentWidget(ActAndWaitUntilAssertion actAndWaitUntilAssertion)
        {
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
        }

        public void DismissMessage()
        {
            _actAndWaitUntilAssertion.ClickAndWaitForElement(
                CookieConsentXPathSelector,
                CookieConsentedXPathSelector, 
                "cookie consent dialog failed to be consented");
        }
    }
}