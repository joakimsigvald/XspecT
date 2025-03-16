using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.IsEnumerable;

public class WhenNotEqualTo : Spec
{
    [Fact]
    public void GivenNotEqualTo_ThenDoesNotThrow() => (new int[] { 1 }).Is().Not().EqualTo([2]).And.Not().Empty();

    [Fact]
    public void GivenEqualTo_ThenGetException()
    {
        int[] arr = [1, 2];
        int[] secondArr = [1, 2];
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => arr.Is().Not().EqualTo(secondArr));
        ex.Message.Is($"Arr is not equal to secondArr");
        ex.InnerException.Message.Is($"Expected arr to not be equal to [1, 2] but found [1, 2]");
    }
}