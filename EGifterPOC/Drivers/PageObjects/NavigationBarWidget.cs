using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace EGifterPOC.Drivers.PageObjects
{
    public class NavigationBarWidget
    {
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

        private const string containerXPath = "//div[@class='subNavbarLinksComponent']";

        public const string HomeTabXPathSelector = "//a[@class='navbar-brand']";

        private static readonly Dictionary<Tab, string> TabTextDictionary = new Dictionary<Tab, string>
        {
            {Tab.BuyGiftCards, "Buy Gift Cards"},
            {Tab.Deals, "Deals"},
            {Tab.Promos, "Promos"},
            {Tab.GroupGift, "Group Gift"},
            {Tab.BuyWithBitcoin, "Buy With Bitcoin"},
            {Tab.MerchantSolutions, "Merchant Solutions"},
            {Tab.BuyForBusiness, "Buy For Business"}
        };

        private readonly ActAndWaitUntilAssertion _actAndWaitUntilAssertion;

        public NavigationBarWidget(ActAndWaitUntilAssertion actAndWaitUntilAssertion)
        {
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
        }

        public string TabText(Tab tab)
        {
            return TabTextDictionary[tab];
        }

        private string TabKeyAttributeXPathFragment(Tab tab)
        {
            return $"[@token='{TabText(tab)}']";
        }

        private string TabXPathSelector(Tab tab)
        {
            return $"{containerXPath}//a[span{TabKeyAttributeXPathFragment(tab)}]";
        }

        private string SelectedTabXPathSelector(Tab tab)
        {
            return
                $"{containerXPath}//a[contains(@class,'router-link-exact-active')][span{TabKeyAttributeXPathFragment(tab)}]";
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

        [Binding]
        public class StringToTabTransformations
        {
            [StepArgumentTransformation]
            public Tab TransformTabStringToTab(string tabString)
            {
                if (TabTextDictionary.All(kv => kv.Value != tabString))
                {
                    var values = string.Concat(TabTextDictionary.Values.Select((s, i) => i == 0 ? s : ", " + s));
                    throw new ArgumentException(
                        $"{tabString} is not a valid tab, tab must be one of the following: {values}");
                }

                return TabTextDictionary.First(kv => kv.Value == tabString).Key;
            }
        }
    }
}