using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.IsEnumerable;

public class WhenNotNull : Spec
{
    [Fact]
    public void GivenNotNull_ThenDoesNotThrow() => Zero<int>().Is().Not().Null().And.Empty();

    [Fact]
    public void GivenNull_ThenGetException()
    {
        int[] arr = null;
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => arr.Is().Not().Null());
        ex.Message.Is($"Arr is not null");
        ex.HasInnerMessage($"Expected arr to not be null but found null");
    }
}