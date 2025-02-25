using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.IsEnumerable;

public class WhenNot : Spec
{
    [Fact]
    public void GivenNotSameInstance_ThenDoesNotThrow() => (new int[] {1}).Is().Not([1]).And.NotEmpty();

    [Fact]
    public void GivenSameInstance_ThenGetException()
    {
        int[] arr = [1, 2];
        var arrAgain = arr;
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => arr.Is().Not(arrAgain));
        ex.Message.Is($"Arr is not arrAgain");
        ex.InnerException.Message.Is($"Expected arr to be not [1, 2] but found [1, 2]");
    }
}