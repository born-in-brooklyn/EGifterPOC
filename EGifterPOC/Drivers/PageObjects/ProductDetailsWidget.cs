namespace EGifterPOC.Drivers.PageObjects
{
    public class ProductDetailsWidget
    {
        private const string ContainerXPath = "//div[contains(@class,'cardDetailsComponent')]";
        private const string DenominationPickerXPath = "//div[contains(@class,'denominationPickerComponent')]";

        private const string DenominationQuickSelectXPath =
            "//div[contains(@class,'denominationQuickSelectComponent')]";

        private const string BuyForMyselfButtonXPath =
            ContainerXPath + "//span[@token='GiftCardCatalog_CardDetails_BuyForMyselfButton']";

        private const string ToMyselfCartItemLabel = "//span[@token='CartLineItem_MyselfLabel']";
        private readonly ActAndWaitUntilAssertion _actAndWaitUntilAssertion;

        public ProductDetailsWidget(ActAndWaitUntilAssertion actAndWaitUntilAssertion)
        {
            _actAndWaitUntilAssertion = actAndWaitUntilAssertion;
        }

        private string DenominationQuickSelectButtonXPath(string value)
        {
            return
                $"{ContainerXPath}{DenominationPickerXPath}{DenominationQuickSelectXPath}//button[contains(@class,'quickSelectButton')][contains(normalize-space(),'{value}')]";
        }

        private string SelectedDenominationQuickSelectButtonXPath(string value)
        {
            return
                $"{ContainerXPath}{DenominationPickerXPath}{DenominationQuickSelectXPath}//button[contains(@class,'quickSelectButton')][contains(@class,'active')][contains(normalize-space(),'{value}')]";
        }

        public void QuickPickDenomination(string value)
        {
            _actAndWaitUntilAssertion.ClickAndWaitForElement(DenominationQuickSelectButtonXPath(value),
                SelectedDenominationQuickSelectButtonXPath(value),
                $"a {value} quick pick denomination could not be selected");
        }

        public void BuyForMyself()
        {
            _actAndWaitUntilAssertion.ClickAndWaitForElement(BuyForMyselfButtonXPath, ToMyselfCartItemLabel,
                "failed to click 'BUY FOR MYSELF' button");
        }
    }
}