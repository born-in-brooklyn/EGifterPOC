using System;
using System.Text;
using EGifterPOC.Steps;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace EGifterPOC.Drivers.PageObjects
{
    public class LineItemsWidget
    {
        private const string ContainerXPath = "//div[@class='cartLineItems']";

        private readonly RemoteWebDriver _remoteWebDriver;
        private readonly ActAndWaitUntilAssertion _actAndWaitUntilAssertion;

        public LineItemsWidget(RemoteWebDriver remoteWebDriver, ActAndWaitUntilAssertion actAndWaitUntilAssertion)
        {
            _remoteWebDriver = remoteWebDriver;
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
        }

        private string LineItemXPathFragment(int index)
        {
            return $"div[@class='cartLineItemComponent'][{index}]";
        }

        private string TotalValueXPathFragment(string totalValue)
        {
            return $"//div[contains(@class, 'totalValue') and //*[normalize-space(text())='{totalValue}']]";
        }

        private string ItemNameXPathFragment(string itemName)
        {
            return $"//h3[normalize-space(text())='{itemName}']";
        }

        private string QuantityDropdownXPathFragment(int quantity)
        {
            return $"//div[contains(@class,'quantityDropdown') and //*[normalize-space(text())='{quantity}']]";
        }

        private string ToLabelXPathFragment(string to)
        {
            return $"//p[contains(@class,'toField') and //*[normalize-space(text())='{to}']]";
        }

        private string ValueLabelXPathFragment(string value)
        {
            return $"//p[contains(@class,'valueField') and //*[normalize-space(text())='{value}']]";
        }


        private string MatchesLineItemExpectationsXPath(int index, ShoppingCartItem expected)
        {
            var retval = new StringBuilder();
            retval.Append(ContainerXPath);
            retval.Append("/div[@class='cartLineItemComponent' ");
            retval.Append($"and position()={index+1} ");
            retval.Append($"and .{ItemNameXPathFragment(expected.Name)} ");
            retval.Append($"and .{ToLabelXPathFragment(expected.To)} ");
            retval.Append($"and .{ValueLabelXPathFragment(expected.Value)} ");
            retval.Append($"and .{TotalValueXPathFragment(expected.Total)} ");
            retval.Append($"and .{QuantityDropdownXPathFragment(expected.Quantity)}]");

            return retval.ToString();
        }

        public bool MatchesItemExpectations(int index, ShoppingCartItem expected)
        {
            try
            {
                var xpath = MatchesLineItemExpectationsXPath(index, expected);
                _remoteWebDriver.FindElementByXPath(xpath);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}