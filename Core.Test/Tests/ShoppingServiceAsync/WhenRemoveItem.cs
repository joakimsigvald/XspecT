using Xunit;
using XspecT.Test.Subjects;
using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.Tests.AsyncShoppingService;

public abstract class WhenRemoveItem : ShoppingServiceAsyncSpec<ShoppingCart>
{
    protected int CartId = 123;
    protected ShoppingCartItem[] CartItems;
    protected readonly ShoppingCartItem Item = new("X");
    private ShoppingCart _cart;

    protected WhenRemoveItem() => When(() => SUT.RemoveFromCart(CartId, Cart.Items[0]));

    protected ShoppingCart Cart => _cart ??= new() { Id = CartId, Items = CartItems };

    protected override void Setup()
        => GetMock<IShoppingCartRepository>()
        .Setup(repo => repo.GetCart(CartId))
        .ReturnsAsync(new ShoppingCart { Id = CartId, Items = CartItems });

    public class GivenCartWithOneItem : WhenRemoveItem
    {
        public GivenCartWithOneItem() => Given(() => CartItems = new[] { new ShoppingCartItem("X") });
        [Fact] public void ThenCartIsEmpty() => Result.Items.IsEmpty();
    }
}