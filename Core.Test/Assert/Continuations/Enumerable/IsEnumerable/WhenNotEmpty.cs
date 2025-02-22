using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.IsEnumerable;

public class WhenNotEmpty : Spec
{
    [Fact]
    public void GivenNotEmpty_ThenDoesNotThrow() => One<int>().Is().NotEmpty().And.Has().Single();

    [Fact]
    public void GivenEmpty_ThenGetException()
    {
        var arr = Zero<int>();
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => arr.Is().NotEmpty());
        ex.Message.Is($"Arr is not empty");
        ex.InnerException.Message.Is($"Expected arr to be not empty but found []");
    }
}