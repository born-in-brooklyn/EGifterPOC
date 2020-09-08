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
                _remoteWebDriver.FindElementByXPath(
                    $"{ContainerXPath}//span[@key='Cart_TotalItems'][text()={expectedItemTotal}]");
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
                    $"{ContainerXPath}//div[@class='h2 pull-right'][normalize-space(text())='{expectedTotalAmount}']");
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            return true;
        }

    }
}