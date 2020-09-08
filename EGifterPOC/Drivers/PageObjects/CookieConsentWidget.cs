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

        private readonly RemoteWebDriver _webDriver;

        public CookieConsentWidget(RemoteWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void DismissMessage()
        {
            var toClick = _webDriver.FindElementByXPath(CookieConsentXPathSelector);
            toClick.Click();
            Thread.Sleep(2000);
        }
    }
}