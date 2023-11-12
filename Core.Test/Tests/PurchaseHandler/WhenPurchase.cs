using Moq;
using XspecT.Assert;
using XspecT.Test.Subjects.Order;
using XspecT.Test.Subjects.Purchase;

namespace XspecT.Test.Tests.PurchaseHandler;

public class WhenPurchase : PurchaseHandlerSpec<PurchaseResponseModel>
{
    protected int BasketId;
    protected Checkout Checkout;

    protected WhenPurchase() => When(_ => _.Purchase(BasketId))
        .Given<ICheckoutProvider>().That(_ => _.GetExistingCheckout(BasketId)).Returns(() => Checkout)
        .And<IBasketRepository>().That(_ => _.GetEditable(BasketId)).Returns(() => Checkout.Basket);

    public class GivenEditableBasket : WhenPurchase
    {
        public GivenEditableBasket()
            => Given(() => Checkout = new Checkout() { Basket = new() { Id = BasketId }, IsOpen = true }).
            And(() => BasketId = 123);

        [Fact]
        public void ThenPublishBasketPurchasedEventAndCheckoutIsClosed()
            => Then<ITopicExchangeV2<BasketPurchasedV1>>(_ => _.Publish(It.IsAny<BasketPurchasedV1>()))
            .And(this).Checkout.IsOpen.Is().False();
    }
}