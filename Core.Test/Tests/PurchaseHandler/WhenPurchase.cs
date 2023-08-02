using Moq;
using XspecT.Test.Subjects.Purchase;
using XspecT.Test.Subjects.PurchaseOrder;
using XspecT.Verification;

namespace XspecT.Test.Tests.PurchaseHandler;

public class WhenPurchase : PurchaseHandlerSpec<PurchaseResponseModel>
{
    protected int BasketId;
    protected Checkout Checkout;

    protected WhenPurchase() => When(() => SUT.Purchase(BasketId));

    protected override void Setup()
    {
        The<ICheckoutProvider>()
            .Setup(provider => provider.GetExistingCheckout(BasketId))
            .ReturnsAsync(Checkout);
        The<IBasketRepository>().Setup(repo => repo.GetEditable(BasketId)).ReturnsAsync(Checkout.Basket);
    }

    public class GivenEditableBasket : WhenPurchase
    {
        public GivenEditableBasket()
            => GivenThat(() => Checkout = new Checkout() { Basket = new() { Id = BasketId }, IsOpen = true }).
            GivenThat(() => BasketId = 123);

        [Fact]
        public void ThenPublishBasketPurchasedEvent()
            => Then().Does<ITopicExchangeV2<BasketPurchasedV1>>(_ => _.Publish(It.IsAny<BasketPurchasedV1>()));

        [Fact] public void ThenCheckoutIsClosed() => Then(this).Checkout.IsOpen.Is().False();
    }
}