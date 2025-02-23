using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.IsEnumerable;

public class WhenNotEqualTo : Spec
{
    [Fact]
    public void GivenNotEqualTo_ThenDoesNotThrow() => (new int[] {1}).Is().NotEqualTo([2]).And.NotEmpty();

    [Fact]
    public void GivenEqualTo_ThenGetException()
    {
        int[] arr = [1, 2];
        int[] secondArr = [1, 2];
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => arr.Is().NotEqualTo(secondArr));
        ex.Message.Is($"Arr is not equal to secondArr");
        ex.InnerException.Message.Is($"Expected arr to be not equal to [1, 2] but found [1, 2]");
    }
}