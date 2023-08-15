using XspecT.Test.Subjects;
using XspecT.Verification;
using Moq;

namespace XspecT.Test.Tests.AsyncShoppingService;

public abstract class WhenRemoveItem : ShoppingServiceAsyncSpec<ShoppingCart>
{
    protected int CartId = 123;
    protected ShoppingCartItem[] CartItems;
    protected readonly ShoppingCartItem Item = new("X");
    private ShoppingCart _cart;

    protected WhenRemoveItem()
        => When(_ => _.RemoveFromCart(CartId, Cart.Items[0]))
        .GivenThat(() => TheMocked<IShoppingCartRepository>()
        .Setup(repo => repo.GetCart(CartId))
        .ReturnsAsync(new ShoppingCart { Id = CartId, Items = CartItems }));

    protected ShoppingCart Cart => _cart ??= new() { Id = CartId, Items = CartItems };

    public class GivenCartWithOneItem : WhenRemoveItem
    {
        public GivenCartWithOneItem() => GivenThat(() => CartItems = new[] { new ShoppingCartItem("X") });
        [Fact] public void ThenCartIsEmpty() => Result.Items.Is().Empty();
    }
}