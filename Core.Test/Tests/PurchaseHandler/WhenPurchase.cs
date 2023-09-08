﻿using Moq;
using XspecT.Test.Subjects.Order;
using XspecT.Test.Subjects.Purchase;
using XspecT.Verification;

namespace XspecT.Test.Tests.PurchaseHandler;

public class WhenPurchase : PurchaseHandlerSpec<PurchaseResponseModel>
{
    protected int BasketId;
    protected Checkout Checkout;

    protected WhenPurchase() => When(_ => _.Purchase(BasketId))
        .GivenThat<ICheckoutProvider>(_ => _.Setup(_ => _.GetExistingCheckout(BasketId))
        .ReturnsAsync(Checkout))
        .And<IBasketRepository>(_ => _.Setup(_ => _.GetEditable(BasketId))
        .ReturnsAsync(Checkout.Basket));

    public class GivenEditableBasket : WhenPurchase
    {
        public GivenEditableBasket()
            => GivenThat(() => Checkout = new Checkout() { Basket = new() { Id = BasketId }, IsOpen = true }).
            And(() => BasketId = 123);

        [Fact]
        public void ThenPublishBasketPurchasedEventAndCheckoutIsClosed()
            => Then<ITopicExchangeV2<BasketPurchasedV1>>(_ => _.Publish(It.IsAny<BasketPurchasedV1>()))
            .And(this).Checkout.IsOpen.Is().False();
    }
}