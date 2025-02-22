using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.IsEnumerable;

public class WhenEqualTo : Spec
{
    [Fact]
    public void GivenEqualTo_ThenDoesNotThrow() => (new int[] {1, 2}).Is().EqualTo([1, 2]).And.Is().NotEmpty();

    [Fact]
    public void GivenNotEqualTo_ThenGetException()
    {
        int[] arr = [1, 2];
        int[] secondArr = [1, 3];
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => arr.Is().EqualTo(secondArr));
        ex.Message.Is($"Arr is equal to secondArr");
        ex.InnerException.Message.Is($"Expected arr to be equal to [1, 3] but found [1, 2]");
    }
}