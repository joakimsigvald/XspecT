using XspecT.Test.Subjects.Shopping;

namespace XspecT.Test.Tests.BasketItemFactory;

public class WhenCreateBasketItems : BasketItemFactorySpec<BasketItem[]>
{
    private const int _customerId = 1;
    private const int _companyId = 2;
    protected NewBasketItem[] NewBasketItems;

    protected WhenCreateBasketItems() => When(() => SUT.CreateBasketItems(_customerId, _companyId, NewBasketItems));

    public class GivenItemWithUnknownProduct : WhenCreateBasketItems
    {
        public GivenItemWithUnknownProduct()
            => GivenThat(() => NewBasketItems = new[] { new NewBasketItem { PartNo = "P123" } }).
            Using(Array.Empty<Product>());

        [Fact] public void ThenThrowBasketItemNotBuyable() => Then().Throws<BasketItemNotBuyable>();
    }
}