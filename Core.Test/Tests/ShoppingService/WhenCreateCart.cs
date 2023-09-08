using XspecT.Test.Subjects;
using XspecT.Verification;

namespace XspecT.Test.Tests.ShoppingService;

public abstract class WhenCreateCart : ShoppingServiceSpec<ShoppingCart>
{
    protected int Id;

    protected WhenCreateCart() => When(_ => _.CreateCart(Id));

    public class GivenIdIsOne : WhenCreateCart
    {
        public GivenIdIsOne() => GivenThat(() => Id = 1);
        [Fact] public void ThenCartIdIsOne() => Result.Id.Is(1);
        [Fact] public void ThenCartIdIsNotTwo() => Result.Id.Is().Not(2);
    }

    public class GivenIdIsTwo : WhenCreateCart
    {
        public GivenIdIsTwo() => GivenThat(() => Id = 2);
        [Fact] public void ThenCartIdIsTwo() => Result.Id.Is(2);
    }
}