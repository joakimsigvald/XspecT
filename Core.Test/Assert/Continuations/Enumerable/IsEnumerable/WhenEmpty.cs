using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.IsEnumerable;

public class WhenEmpty : Spec
{
    [Fact]
    public void GivenEmpty_ThenDoesNotThrow() => Zero<int>().Is().Empty().And.Empty();

    [Fact]
    public void GivenNotEmpty_ThenGetException()
    {
        int[] arr = [1];
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => arr.Is().Empty());
        ex.Message.Is($"Arr is empty");
        ex.InnerException.Message.Is($"Expected arr to be empty but found [1]");
    }
}