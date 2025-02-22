using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Continuations.Enumerable.HasEnumerable;

public class WhenCount : Spec
{
    [Fact]
    public void GivenCorrectCount_ThenDoesNotThrow() => Two<int>().Has().Count(2).And.Is().NotEmpty();

    [Fact]
    public void GivenWrongCount_ThenGetException()
    {
        int[] arr = [1];
        var ex = Xunit.Assert.Throws<XunitException>(() => arr.Has().Count(2));
        ex.Message.Is($"Arr has count 2");
        ex.InnerException.Message.Is($"Expected arr to have count 2 but found [1]");
    }
}