using Moq;
using XspecT.Test.Subjects.Purchase;
using XspecT.Test.Subjects.PurchaseOrder;

namespace XspecT.Test.Tests.PurchaseHandler;

public class WhenPurchase : PurchaseHandlerSpec<PurchaseResponseModel>
{
    protected Basket Basket;

    protected WhenPurchase() => When(() => SUT.Purchase(Basket.Id));

    protected override void Setup()
    {
        The<ICheckoutProvider>()
            .Setup(provider => provider.GetExistingCheckout(Basket.Id))
            .ReturnsAsync(new Checkout() { Basket = Basket });
        The<IBasketRepository>().Setup(repo => repo.GetEditable(Basket.Id)).ReturnsAsync(Basket);
    }

    public class GivenEditableBasket : WhenPurchase
    {
        [Fact]
        public void ThenPublishBasketPurchasedEvent()
            => GivenThat(() => Basket = new() { Id = 1 }).
            Then.Does<ITopicExchangeV2<BasketPurchasedV1>>(_ => _.Publish(It.IsAny<BasketPurchasedV1>()));
    }
}