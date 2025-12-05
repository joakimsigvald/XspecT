using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.HasEnumerable;

public class WhenCount : Spec
{
    [Fact]
    public void GivenCorrectCount_ThenDoesNotThrow() => Two<int>().Has().Count(2).And.Count(2);

    [Fact]
    public void GivenWrongCount_ThenGetException()
    {
        int[] arr = [1];
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => arr.Has().Count(2));
        ex.HasMessage($"Expected arr to have count 2 but found 1: [1]", "Arr has count 2");
    }
}