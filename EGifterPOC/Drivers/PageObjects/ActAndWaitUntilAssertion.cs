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
        private readonly Configuration _configuration;

        public TimeSpan PauseBetweenActAndAssert { get; set; }
        public TimeSpan WaitForAssert { get; set; }
        public ActAndWaitUntilAssertion(RemoteWebDriver webDriver, Configuration configuration)
        {
            _webDriver = webDriver;
            _configuration = configuration;
            PauseBetweenActAndAssert = configuration.DefaultPauseBetweenActAndAssert;
            WaitForAssert = configuration.DefaultWaitForAssert;
        }

        private void ActActionAndWaitForAction(Action<RemoteWebDriver> ActAction,
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
                    ActAction(d);

                    Thread.Sleep(PauseBetweenActAndAssert);

                    return waitForAssertFunc(d);
                }
                catch (WebDriverException)
                {
                    return false;
                }
            });
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

        public void GoToUrlAndWaitForElement(Uri urlToGoTo, string elementToWaitForXPathSelector, string errorMessage = null)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                errorMessage = $"element {elementToWaitForXPathSelector} failed to appear";
            }

            ActActionAndWaitForAction(
                d => d.Navigate().GoToUrl(urlToGoTo),
                BuildWaitForElementFunc(elementToWaitForXPathSelector),
            errorMessage);
        }

        public void ClickAndWaitForElementAbsence(string elementToClickXPathSelector, string elementToWaitForXPathSelector = null, string errorMessage = null)
        {
            if (string.IsNullOrWhiteSpace(elementToClickXPathSelector))
            {
                throw new ArgumentNullException("elementToClickXPathSelector");
            }

            if (string.IsNullOrWhiteSpace(elementToWaitForXPathSelector))
            {
                elementToWaitForXPathSelector = elementToClickXPathSelector;
            }

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                errorMessage = $"element {elementToWaitForXPathSelector} is still present";
            }

            ActActionAndWaitForAction(
                d => d.FindElementByXPath(elementToClickXPathSelector).Click(),
                BuildWaitForElementAbsenceFunc(elementToWaitForXPathSelector),
                errorMessage);
        }

        public void ClickAndWaitForElement(string elementToClickXPathSelector, string elementToWaitForXPathSelector = null, string errorMessage = null)
        {
            if (string.IsNullOrWhiteSpace(elementToClickXPathSelector))
            {
                throw new ArgumentNullException("elementToClickXPathSelector");
            }

            if (string.IsNullOrWhiteSpace(elementToWaitForXPathSelector))
            {
                elementToWaitForXPathSelector = elementToClickXPathSelector;
            }

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                errorMessage = $"element {elementToWaitForXPathSelector} was not found";
            }

            ActActionAndWaitForAction(
                d => d.FindElementByXPath(elementToClickXPathSelector).Click(),
                BuildWaitForElementFunc(elementToWaitForXPathSelector),
                errorMessage);
        }

        public void ClearElementAndWaitForElement(string fieldXPathSelector, string elementToWaitForXPathSelector, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(fieldXPathSelector))
            {
                throw new ArgumentNullException("fieldXPathSelector");
            }

            if (string.IsNullOrWhiteSpace(elementToWaitForXPathSelector))
            {
                throw new ArgumentNullException("elementToWaitForXPathSelector");
            }

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                errorMessage = $"element {elementToWaitForXPathSelector} was not found";
            }

            ActActionAndWaitForAction(
                d => d.FindElementByXPath(fieldXPathSelector).Clear(),
                BuildWaitForElementFunc(elementToWaitForXPathSelector),
                errorMessage);
        }

        public void SendKeysToElementAndWaitForElement(string fieldXPathSelector, string keysToSend, string elementToWaitForXPathSelector, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(fieldXPathSelector))
            {
                throw new ArgumentNullException("fieldXPathSelector");
            }

            if (string.IsNullOrWhiteSpace(keysToSend))
            {
                throw new ArgumentNullException("keysToSend");
            }

            if (string.IsNullOrWhiteSpace(elementToWaitForXPathSelector))
            {
                throw new ArgumentNullException("elementToWaitForXPathSelector");
            }

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                errorMessage = $"element {elementToWaitForXPathSelector} was not found";
            }

            ActActionAndWaitForAction(
                d => d.FindElementByXPath(fieldXPathSelector).SendKeys(keysToSend),
                BuildWaitForElementFunc(elementToWaitForXPathSelector),
                errorMessage);
        }

    }
}
