using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.IsEnumerable;

public class WhenNotNull : Spec
{
    [Fact]
    public void GivenNotNull_ThenDoesNotThrow() => Zero<int>().Is().NotNull().And.Empty();

    [Fact]
    public void GivenNull_ThenGetException()
    {
        int[] arr = null;
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => arr.Is().NotNull());
        ex.Message.Is($"Arr is not null");
        ex.InnerException.Message.Is($"Expected arr to be not null but found null");
    }
}