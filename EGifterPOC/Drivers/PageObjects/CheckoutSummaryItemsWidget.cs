using System;
using System.Text;
using System.Threading;
using EGifterPOC.Steps;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace EGifterPOC.Drivers.PageObjects
{
    public class CheckoutSummaryItemsWidget
    {
        private const string ContainerXPath = "//div[@class='summaryGiftCards']";

        private readonly ActAndWaitUntilAssertion _actAndWaitUntilAssertion;

        public CheckoutSummaryItemsWidget(ActAndWaitUntilAssertion actAndWaitUntilAssertion)
        {
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
        }

        private string ItemNameXPathFragment(string itemName)
        {
            return $"//p[@class='checkoutSummaryCell' and //*[normalize-space(text())='{itemName}']]";
        }

        private string QuantityXPathFragment(int quantity)
        {
            return $"//p[@class='checkoutSummaryCell' and //*[normalize-space(text())='{quantity}']]";
        }

        private string AmountXPathFragment(string value)
        {
            return $"//p[contains(@class,'totalAmt') and //*[normalize-space(text())='{value}']]";
        }

        private string MatchesSummaryItemExpectationsXPath(int index, CheckoutSummaryItem expected)
        {
            var retval = new StringBuilder();
            retval.Append(ContainerXPath);
            retval.Append($"/div[contains(@class,'giftCardSummaryRowComponent')][position()={index+1} ");
            retval.Append($"and .{ItemNameXPathFragment(expected.Name)} ");
            retval.Append($"and .{QuantityXPathFragment(expected.Quantity)} ");
            retval.Append($"and .{AmountXPathFragment(expected.Amount)}]");

            return retval.ToString();
        }

        public bool MatchesItemExpectations(int index, CheckoutSummaryItem expected)
        {
            try
            {
                _actAndWaitUntilAssertion.WaitForElementWithRetry(MatchesSummaryItemExpectationsXPath(index, expected));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}