using Moq;
using XspecT.Test.Subjects.Purchase;
using XspecT.Test.Subjects.PurchaseOrder;

namespace XspecT.Test.Tests.PurchaseHandler;

public class WhenPurchase : PurchaseHandlerSpec<PurchaseResponseModel>
{
    protected const int BasketId = 1;
    protected Checkout Checkout;

    protected WhenPurchase() => Using(new Basket()).When(() => SUT.Purchase(BasketId));

    protected override void Setup()
    {
        The<ICheckoutProvider>()
            .Setup(provider => provider.GetExistingCheckout(BasketId))
            .ReturnsAsync(Checkout);
        The<IBasketRepository>()
            .Setup(repo => repo.GetEditable(BasketId))
            .ReturnsAsync(new Basket { Id = BasketId });
    }

    public class GivenEditableBasket : WhenPurchase
    {
        protected override void Set() => Checkout = CreateCheckout();

        [Fact]
        public void ThenPublishBasketPurchasedEvent()
            => Then.Does<ITopicExchangeV2<BasketPurchasedV1>>(topic =>
            topic.Publish(It.IsAny<BasketPurchasedV1>()));
    }

    private static Checkout CreateCheckout(params BasketItem[] items)
        => new()
        {
            Basket = new()
            {
                Id = BasketId,
                Items = items
            }
        };
}