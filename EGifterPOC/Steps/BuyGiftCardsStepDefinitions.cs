using EGifterPOC.Drivers.PageObjects;
using TechTalk.SpecFlow;

namespace EGifterPOC.Steps
{
    [Binding]
    public sealed class BuyGiftCardsStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        private readonly BuyGiftCardsPageObject _buyGiftCardsPageObject;

        public BuyGiftCardsStepDefinitions(BuyGiftCardsPageObject buyGiftCardsPageObject)
        {
            _buyGiftCardsPageObject = buyGiftCardsPageObject;
        }

        [StepDefinition("I load the 'Buy Gift Cards' page")]
        public void ILoadTheBuyGiftCardsPage()
        {
            _buyGiftCardsPageObject.Load();
        }

        [StepDefinition(@"I select the ""(.*)"" category")]
        public void ISelectTheCategory(string category)
        {
            _buyGiftCardsPageObject.CategoriesMenuWidget.ClickCategory(category);
        }

        [StepDefinition(@"I search for ""(.*)""")]
        public void ISearchFor(string toSearchFor)
        {
            _buyGiftCardsPageObject.ProductSearchWidget.Search(toSearchFor);
        }

        [StepDefinition(@"I clear search")]
        public void IClearSearch()
        {
            _buyGiftCardsPageObject.ProductSearchWidget.ClearSearch();
        }

        [StepDefinition(@"I select the ""(.*)"" card from the catalog")]
        public void ISelectTheCardFromTheCatalog(string brandName)
        {
            _buyGiftCardsPageObject.ProductGridWidget.Select(brandName);
        }

        [StepDefinition(@"I select (.*) as the value")]
        public void ISelectAsTheValue(string value)
        {
            _buyGiftCardsPageObject.ProductDetailsWidget.QuickPickDenomination(value);
        }

        [StepDefinition(@"I click the 'Buy for Myself' button")]
        public void IClickTheBuyForMyselfButton()
        {
            _buyGiftCardsPageObject.ProductDetailsWidget.BuyForMyself();
        }
    }
}