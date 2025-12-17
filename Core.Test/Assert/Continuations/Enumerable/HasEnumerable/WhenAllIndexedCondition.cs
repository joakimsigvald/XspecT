using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Continuations.Enumerable.HasEnumerable;

public class WhenAllIndexedCondition : Spec
{
    [Fact]
    public void GivenEmpty_ThenDoesNotThrow() 
        => Zero<int>().Has().All((it, i) => it == i + 1).And.Is().Empty();

    [Fact]
    public void GivenAllSatisfyCondition_ThenDoesNotThrow()
    {
        int[] arr = [1, 2];
        arr.Has().All((it, i) => it == i + 1).And.Is().Not().Empty();
    }

    [Fact]
    public void GivenConditionNotSatisfiedForAll_ThenGetException()
    {
        int[] arr = [1, 3];
        var ex = Xunit.Assert.Throws<XunitException>(() => arr.Has().All((it, i) => it == i + 1));
        ex.HasMessage($"Expected arr to have all elements satisfying the condition but found [1, 3]",
            "Arr has all (it, i) => it == i + 1");
    }

    [Fact]
    public void GivenConditionNotSatisfiedForAny_ThenGetException()
    {
        int[] arr = [2, 3];
        var ex = Xunit.Assert.Throws<XunitException>(() => arr.Has().Some((it, i) => it == i + 1));
        ex.HasMessage($"Expected arr to have some element satisfying the condition but found [2, 3]",
            "Arr has some (it, i) => it == i + 1");
    }
}