using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace EGifterPOC.Drivers.PageObjects
{
    [Binding]
    public class ActAndWaitUntilAssertion
    {
        private readonly RemoteWebDriver _webDriver;

        public ActAndWaitUntilAssertion(RemoteWebDriver webDriver, Configuration configuration)
        {
            _webDriver = webDriver;
            PauseBetweenActAndAssert = configuration.DefaultPauseBetweenActAndAssert;
            WaitForAssert = configuration.DefaultWaitForAssert;
        }

        public TimeSpan PauseBetweenActAndAssert { get; set; }
        public TimeSpan WaitForAssert { get; set; }

        private void ActActionAndWaitForAction(Action<RemoteWebDriver> actAction,
            Func<RemoteWebDriver, bool> waitForAssertFunc, string errorMessage)
        {
            var wait = new DefaultWait<RemoteWebDriver>(_webDriver)
            {
                Message = errorMessage,
                PollingInterval = PauseBetweenActAndAssert,
                Timeout = WaitForAssert
            };
            wait.Until(d =>
            {
                try
                {
                    actAction(d);

                    Thread.Sleep(PauseBetweenActAndAssert);

                    return waitForAssertFunc(d);
                }
                catch (WebDriverException)
                {
                    return false;
                }
            });
        }

        private void ActionAndWaitForElement(Action<RemoteWebDriver> actAction, string elementToWaitForXPathSelector,
            string errorMessage)
        {
            ActActionAndWaitForAction(d =>
            {
                try
                {
                    d.FindElementByXPath(elementToWaitForXPathSelector);
                }
                catch (NoSuchElementException)
                {
                    actAction(d);
                }
            }, BuildWaitForElementFunc(elementToWaitForXPathSelector), errorMessage);
        }

        private Func<RemoteWebDriver, bool> BuildWaitForElementFunc(string elementToWaitForXPathSelector)
        {
            return d =>
            {
                d.FindElementByXPath(elementToWaitForXPathSelector);
                return true;
            };
        }

        private Func<RemoteWebDriver, bool> BuildWaitForElementAbsenceFunc(string elementToWaitForXPathSelector)
        {
            return d =>
            {
                try
                {
                    d.FindElementByXPath(elementToWaitForXPathSelector);
                }
                catch (NoSuchElementException)
                {
                    return true;
                }

                return false;
            };
        }

        public void GoToUrlAndWaitForElement(Uri urlToGoTo, string elementToWaitForXPathSelector,
            string errorMessage = null)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
                errorMessage = $"element {elementToWaitForXPathSelector} failed to appear";

            ActionAndWaitForElement(
                d => d.Navigate().GoToUrl(urlToGoTo),
                elementToWaitForXPathSelector,
                errorMessage);
        }

        public void ClickAndWaitForElementAbsence(string elementToClickXPathSelector,
            string elementToWaitForXPathSelector = null, string errorMessage = null)
        {
            if (string.IsNullOrWhiteSpace(elementToClickXPathSelector))
                throw new ArgumentNullException("elementToClickXPathSelector");

            if (string.IsNullOrWhiteSpace(elementToWaitForXPathSelector))
                elementToWaitForXPathSelector = elementToClickXPathSelector;

            if (string.IsNullOrWhiteSpace(errorMessage))
                errorMessage = $"element {elementToWaitForXPathSelector} is still present";

            ActActionAndWaitForAction(
                d => d.FindElementByXPath(elementToClickXPathSelector).Click(),
                BuildWaitForElementAbsenceFunc(elementToWaitForXPathSelector),
                errorMessage);
        }

        public void ClickAndWaitForElement(string elementToClickXPathSelector,
            string elementToWaitForXPathSelector = null, string errorMessage = null)
        {
            if (string.IsNullOrWhiteSpace(elementToClickXPathSelector))
                throw new ArgumentNullException("elementToClickXPathSelector");

            if (string.IsNullOrWhiteSpace(elementToWaitForXPathSelector))
                elementToWaitForXPathSelector = elementToClickXPathSelector;

            if (string.IsNullOrWhiteSpace(errorMessage))
                errorMessage = $"element {elementToWaitForXPathSelector} was not found";

            ActionAndWaitForElement(
                d => d.FindElementByXPath(elementToClickXPathSelector).Click(),
                elementToWaitForXPathSelector,
                errorMessage);
        }

        public void ClearElementAndWaitForElement(string fieldXPathSelector, string elementToWaitForXPathSelector,
            string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(fieldXPathSelector)) throw new ArgumentNullException("fieldXPathSelector");

            if (string.IsNullOrWhiteSpace(elementToWaitForXPathSelector))
                throw new ArgumentNullException("elementToWaitForXPathSelector");

            if (string.IsNullOrWhiteSpace(errorMessage))
                errorMessage = $"element {elementToWaitForXPathSelector} was not found";

            ActionAndWaitForElement(
                d => d.FindElementByXPath(fieldXPathSelector).Clear(),
                elementToWaitForXPathSelector,
                errorMessage);
        }

        public void SendKeysToElementAndWaitForElement(string fieldXPathSelector, string keysToSend,
            string elementToWaitForXPathSelector, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(fieldXPathSelector)) throw new ArgumentNullException("fieldXPathSelector");

            if (string.IsNullOrWhiteSpace(keysToSend)) throw new ArgumentNullException("keysToSend");

            if (string.IsNullOrWhiteSpace(elementToWaitForXPathSelector))
                throw new ArgumentNullException("elementToWaitForXPathSelector");

            if (string.IsNullOrWhiteSpace(errorMessage))
                errorMessage = $"element {elementToWaitForXPathSelector} was not found";

            ActionAndWaitForElement(
                d => d.FindElementByXPath(fieldXPathSelector).SendKeys(keysToSend),
                elementToWaitForXPathSelector,
                errorMessage);
        }
    }
}