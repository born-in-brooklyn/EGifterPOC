using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace EGifterPOC.Drivers.PageObjects
{
    public class TotalSectionWidget
    {
        private const string ContainerXPath = "//section[contains(@class,'totalSection')]";
        private readonly RemoteWebDriver _remoteWebDriver;

        public TotalSectionWidget(RemoteWebDriver remoteWebDriver)
        {
            _remoteWebDriver = remoteWebDriver;
        }

        public bool DoesItemTotalMatch(int expectedItemTotal)
        {
            try
            {
                var selector = $"{ContainerXPath}//span[@token='Cart_TotalItems'][text()='{expectedItemTotal} item(s)']";
                _remoteWebDriver.FindElementByXPath(selector);
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            return true;
        }

        public bool DoesTotalAmountMatch(string expectedTotalAmount)
        {
            try
            {
                _remoteWebDriver.FindElementByXPath(
                    $"{ContainerXPath}//div[@data-testid='Cart_Summary_TotalItemsCost'][normalize-space(text())='{expectedTotalAmount}']");
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            return true;
        }

    }
}