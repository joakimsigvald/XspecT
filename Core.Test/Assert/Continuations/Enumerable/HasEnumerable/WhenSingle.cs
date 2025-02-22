using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Continuations.Enumerable.HasEnumerable;

public class WhenSingle : Spec
{
    [Fact]
    public void GivenSingle_ThenDoesNotThrow() => One<int>().Has().Single().And.Is().NotEmpty();

    [Fact]
    public void GivenEmpty_ThenGetException()
    {
        int[] arr = Zero<int>();
        var ex = Xunit.Assert.Throws<XunitException>(() => arr.Has().Single());
        ex.Message.Is($"Arr has single");
        ex.InnerException.Message.Is($"Expected arr to have single element but found empty");
    }

    [Fact]
    public void GivenTwoElements_ThenGetException()
    {
        int[] arr = [1, 3];
        var ex = Xunit.Assert.Throws<XunitException>(() => arr.Has().Single());
        ex.Message.Is($"Arr has single");
        ex.InnerException.Message.Is($"Expected arr to have single element but found [1, 3]");
    }
}