using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.IsEnumerable;

public class WhenNotEmpty : Spec
{
    [Fact]
    public void GivenNotEmpty_ThenDoesNotThrow() => One<int>().Is().Not().Empty().And.Has().Single();

    [Fact]
    public void GivenEmpty_ThenGetException()
    {
        var arr = Zero<int>();
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => arr.Is().Not().Empty());
        ex.Message.Is($"Arr is not empty");
        ex.InnerException.Message.Is($"Expected arr to not be empty but found []");
    }
}