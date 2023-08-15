using XspecT.Test.Subjects;

namespace XspecT.Test.Tests.AsyncShoppingService;

public abstract class WhenPlaceOrder : ShoppingServiceAsyncSpec<object>
{
    protected ShoppingCart Cart;

    protected WhenPlaceOrder() => When(_ => _.PlaceOrder(Cart));

    public class GivenOpenCart : WhenPlaceOrder
    {
        public GivenOpenCart() => GivenThat(() => Cart = new() { IsOpen = true });
        [Fact] public void ThenOrderIsCreated() => Then().Does<IOrderService>(_ => _.CreateOrder(Cart));
    }

    public class GivenClosedCart : WhenPlaceOrder
    {
        public GivenClosedCart() => GivenThat(() => Cart = new() { IsOpen = false });
        [Fact] public void ThenThrowsNotPurcheable() => Then().Throws<NotPurcheable>();
    }
}