
namespace EGifterPOC.Drivers.PageObjects
{
    public class CategoriesMenuWidget
    {
        private readonly ActAndWaitUntilAssertion _actAndWaitUntilAssertion;
        private const string ContainerXPath = "//div[@class='categoriesMenuComponent']";
        public CategoriesMenuWidget(ActAndWaitUntilAssertion actAndWaitUntilAssertion)
        {
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
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
            return $"{ContainerXPath}//li[a[contains(@class,'router-link-exact-active')]{CategoryTextXPathFragment(category)}]";
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
