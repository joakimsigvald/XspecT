using XspecT.Test.Subjects;
using XspecT.Verification;
using Moq;

namespace XspecT.Test.Tests.AsyncShoppingService;

public abstract class WhenAddItem : ShoppingServiceAsyncSpec<ShoppingCart>
{
    protected int CartId = 123;
    protected ShoppingCartItem[] CartItems;
    protected readonly ShoppingCartItem NewItem = new("N1");

    protected WhenAddItem() => GivenThat(() => TheMocked<IShoppingCartRepository>().Setup(_ => _.GetCart(CartId))
        .ReturnsAsync(new ShoppingCart { Id = CartId, Items = CartItems }))
        .When(_ => _.AddToCart(CartId, NewItem))
        .GivenThat(() => CartItems ??= Array.Empty<ShoppingCartItem>());

    public class GivenEmptyCart : WhenAddItem
    {
        [Fact] public void ThenCartIsNotEmpty() => Result.Items.Is().NotEmpty();
        [Fact] public void ThenCartHasOneItem() => Result.Items.Has().Single();
        [Fact] public void TheIdIsPreserved() => Result.Id.Is(CartId);
        [Fact] public void ThenCartIsStored() => Then().Does<IShoppingCartRepository>(_ => _.StoreCart(Result));
    }

    public class GivenCartWithOneItem : WhenAddItem
    {
        public GivenCartWithOneItem() => GivenThat(() => CartItems = new[] { new ShoppingCartItem("A1") });
        [Fact] public void ThenDoNotThrow() => Then().NotThrows();
        [Fact] public void ThenCartHasTwoItems() => Result.Items.Length.Is(2);
        [Fact] public void ThenNewItemIsLast() => Result.Items.Last().Sku.Is(NewItem.Sku);
        [Fact] public void ThenNewItemIsCloned() => Result.Items.Last().Is().Not(NewItem);
        [Fact] public void ThenItemsAreNotNull() => Result.Items.Has().Only(it => it != null);
        [Fact] public void ThenItemsHaveLineNumbers() => Result.Items.Has().Only((it, i) => it.LineNumber == i + 1);
    }
}