namespace EGifterPOC.Drivers.PageObjects
{
    public class ProductGridWidget
    {
        private const string ContainerXPath = "//div[@class ='giftCardCatalog']";
        private readonly ActAndWaitUntilAssertion _actAndWaitUntilAssertion;

        public ProductGridWidget(ActAndWaitUntilAssertion actAndWaitUntilAssertion)
        {
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
        }

        private string BrandNameXPathSelector(string brandName)
        {
            return
                $"{ContainerXPath}//div[contains(@class,'brandCardComponent')]//div[contains(@class,'brandName')][contains(text(),'{brandName}')]";
        }

        private string ExpandedBrandNameXPathSelector(string brandName)
        {
            return
                $"{ContainerXPath}//div[contains(@class,'expandedCardDetails')]//h3[@class='brandHeading']/a[contains(text(),'{brandName}')]";
        }

        public void Select(string brandName)
        {
            var categoryXPath = BrandNameXPathSelector(brandName);
            var successXPath = ExpandedBrandNameXPathSelector(brandName);

            _actAndWaitUntilAssertion.ClickAndWaitForElement(
                categoryXPath,
                successXPath,
                $"Couldn't select the {brandName} card");
        }
    }
}