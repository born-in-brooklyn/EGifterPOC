namespace EGifterPOC.Drivers.PageObjects
{
    public class ProductSearchWidget
    {
        private const string ContainerXPath = "//div[contains(@class,'giftCardsContainer')]";
        private const string SearchTextBoxXPath = "//input[contains(@class,'searchFieldInput')]";

        private const string FeaturedCardsHeaderXPath =
            "//div[contains(@class,'categoryHeader')]/div/h2[contains(text(),'Featured Cards')]";

        private const string SearchResultHeaderXPath =
            "//div[contains(@class,'categoryHeader')]/div/h2/span[@key='Search Results']";

        private readonly ActAndWaitUntilAssertion _actAndWaitUntilAssertion;

        public ProductSearchWidget(ActAndWaitUntilAssertion actAndWaitUntilAssertion)
        {
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
        }

        public void ClearSearch()
        {
            _actAndWaitUntilAssertion.ClearElementAndWaitForElement(SearchTextBoxXPath, FeaturedCardsHeaderXPath,
                "featured cards header didn't appear");
        }

        public void Search(string toSearchFor)
        {
            _actAndWaitUntilAssertion.SendKeysToElementAndWaitForElement(SearchTextBoxXPath, toSearchFor,
                SearchResultHeaderXPath, "search results header didn't appear");
        }

        private string CategoryTextXPathFragment(string category)
        {
            return $"[contains(text(),'{category}')]";
        }

        private string CategoryXPathSelector(string category)
        {
            return $"{ContainerXPath}//li[a{CategoryTextXPathFragment(category)}]";
        }

        private string SelectedCategoryXPathSelector(string category)
        {
            return
                $"{ContainerXPath}//li[a[contains(@class,'router-link-exact-active')]{CategoryTextXPathFragment(category)}]";
        }

        private string NoneSelectedCategoryXPathSelector()
        {
            return $"{ContainerXPath}[not(//a[contains(@class,'router-link-exact-active')])]";
        }

        public void ClickCategory(string category)
        {
            var categoryXPath = CategoryXPathSelector(category);
            var successXPath = SelectedCategoryXPathSelector(category);

            _actAndWaitUntilAssertion.ClickAndWaitForElement(
                categoryXPath,
                successXPath,
                $"Couldn't click the {category} category");
        }
    }
}