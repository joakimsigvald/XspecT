using XspecT.Test.Subjects.Shopping;

namespace XspecT.Test.Tests.BasketItemFactory;

public class WhenCreateBasketItems : Spec<Subjects.Shopping.BasketItemFactory, BasketItem[]>
{
    protected WhenCreateBasketItems() 
        => When(_ => _.CreateBasketItems(An<int>(), ASecond<int>(), A<NewBasketItem[]>()));

    public class GivenItemWithUnknownProduct : WhenCreateBasketItems
    {
        public GivenItemWithUnknownProduct()
            => Given(One<NewBasketItem>());

        [Fact] public void ThenThrowBasketItemNotBuyable() => Then().Throws<BasketItemNotBuyable>();
    }
}