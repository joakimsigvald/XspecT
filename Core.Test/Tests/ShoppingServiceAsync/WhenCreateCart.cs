using XspecT.Test.Subjects;
using XspecT.Verification;

namespace XspecT.Test.Tests.AsyncShoppingService;

public abstract class WhenCreateCart : ShoppingServiceAsyncSpec<ShoppingCart>
{
    protected int Id;

    protected WhenCreateCart() => When(_ => _.CreateCart(Id));

    public class GivenIdIsOne : WhenCreateCart
    {
        public GivenIdIsOne() => GivenThat(() => Id = 1);
        [Fact] public void ThenCartIdIsOne() => Result.Id.Is(Id);
    }

    public class GivenIdIsTwo : WhenCreateCart
    {
        public GivenIdIsTwo() => GivenThat(() => Id = 2);
        [Fact] public void ThenCartIdIsTwo() => Result.Id.Is(Id);
    }
}