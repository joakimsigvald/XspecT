using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.IsEnumerable;

public class WhenNull : Spec
{
    [Fact]
    public void GivenNull_ThenDoesNotThrow() => ((int[])null).Is().Null().And.Null();

    [Fact]
    public void GivenNotNull_ThenGetException()
    {
        var arr = Zero<int>();
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => arr.Is().Null());
        ex.Message.Is($"Arr is null");
        ex.InnerException.Message.Is($"Expected arr to be null but found []");
    }
}