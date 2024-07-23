using Moq;
using XspecT.Assert;
using XspecT.Test.Subjects.Order;
using XspecT.Test.Subjects.Purchase;

namespace XspecT.Test.Tests.PurchaseHandler;

public class WhenPurchase : Spec<Subjects.Purchase.PurchaseHandler, PurchaseResponseModel>
{
    protected WhenPurchase() 
        => When(_ => _.Purchase(An<int>()))
        .Given<ICheckoutProvider>().That(_ => _.GetExistingCheckout(The<int>())).Returns(() => A<Checkout>())
        .And<IBasketRepository>().That(_ => _.GetEditable(The<int>())).Returns(() => The<Checkout>().Basket);

    public class GivenEditableBasket : WhenPurchase
    {
        public GivenEditableBasket() => Given<Checkout>(_ => _.IsOpen = true);

        [Fact]
        public void ThenPublishBasketPurchasedEventAndCheckoutIsClosed()
            => Then<ITopicExchangeV2<BasketPurchasedV1>>(_ => _.Publish(It.IsAny<BasketPurchasedV1>()))
            .And(The<Checkout>()).IsOpen.Is().False();
    }
}