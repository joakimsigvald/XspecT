using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.HasEnumerable;

public class WhenSingle : Spec
{
    [Fact]
    public void GivenSingle_ThenDoesNotThrow() => One<int>().Has().Single().And.Is().NotEmpty();

    [Fact]
    public void GivenSingle_AndVerifyIt() 
        => One<int>().Has().Single().That.Is(The<int>());

    [Fact]
    public void GivenEmpty_ThenGetException()
    {
        int[] arr = Zero<int>();
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => arr.Has().Single().That.Is(123));
        ex.Message.Is($"Arr has single");
        ex.InnerException.Message.Is($"Expected arr to have single element but found empty");
    }

    [Fact]
    public void GivenTwoElements_ThenGetException()
    {
        int[] arr = [1, 3];
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => arr.Has().Single().That.Is(123));
        ex.Message.Is($"Arr has single");
        ex.InnerException.Message.Is($"Expected arr to have single element but found [1, 3]");
    }
}