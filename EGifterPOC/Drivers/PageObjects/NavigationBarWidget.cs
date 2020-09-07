using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace EGifterPOC.Drivers.PageObjects
{
    public class NavigationBarWidget
    {
        [Binding]
        public class StringToTabTransformations
        {
            [StepArgumentTransformation]
            public Tab TransformTabStringToTab(string tabString)
            {
                if (TabTextDictionary.All(kv => kv.Value != tabString))
                {
                    var values = string.Concat(TabTextDictionary.Values.Select((s, i) => i == 0 ? s : ", " + s));
                    throw new ArgumentException($"{tabString} is not a valid tab, tab must be one of the following: {values}");
                }
                return TabTextDictionary.First(kv => kv.Value == tabString).Key;
            }
        }
        private readonly ActAndWaitUntilAssertion _actAndWaitUntilAssertion;

        private const string containerXPath = "//div[@class='subNavbarLinksComponent']";
        public enum Tab
        {
            Home,
            BuyGiftCards,
            Deals,
            Promos,
            GroupGift,
            BuyWithBitcoin,
            MerchantSolutions,
            BuyForBusiness
        }

        private static readonly Dictionary<Tab, string> TabTextDictionary = new Dictionary<Tab, string>()
        {
            { Tab.BuyGiftCards, "Buy Gift Cards" },
            { Tab.Deals, "Deals" },
            { Tab.Promos, "Promos" },
            { Tab.GroupGift, "Group Gift" },
            { Tab.BuyWithBitcoin, "Buy With Bitcoin" },
            { Tab.MerchantSolutions, "Merchant Solutions" },
            { Tab.BuyForBusiness, "Buy For Business" }
        };

        public NavigationBarWidget(ActAndWaitUntilAssertion actAndWaitUntilAssertion)
        {
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
        }

        public string TabText(Tab tab)
        {
            return TabTextDictionary[tab];
        }

        public const string HomeTabXPathSelector = "//a[@class='navbar-brand']";

        private string TabKeyAttributeXPathFragment(Tab tab)
        {
            return $"[@key='{TabText(tab)}']";
        }

        private string TabXPathSelector(Tab tab)
        {
            return $"{containerXPath}//a[span{TabKeyAttributeXPathFragment(tab)}]";
        }

        private string SelectedTabXPathSelector(Tab tab)
        {
            return $"{containerXPath}//a[contains(@class,'router-link-exact-active')][span{TabKeyAttributeXPathFragment(tab)}]";
        }

        private string NoneSelectedTabXPathSelector()
        {
            return $"{containerXPath}[not(//a[contains(@class,'router-link-exact-active')])]";
        }

        public void ClickTab(Tab tab)
        {
            string tabXPath;
            string successXPath;
            if (tab == Tab.Home)
            {
                tabXPath = HomeTabXPathSelector;
                successXPath = NoneSelectedTabXPathSelector();
            }
            else
            {
                tabXPath = TabXPathSelector(tab);
                successXPath = SelectedTabXPathSelector(tab);
            }

            _actAndWaitUntilAssertion.ClickAndWaitForElement(
                tabXPath, 
                successXPath,
                $"Couldn't click the {Enum.GetName(typeof(Tab), tab)} tab");
        }
    }
}
