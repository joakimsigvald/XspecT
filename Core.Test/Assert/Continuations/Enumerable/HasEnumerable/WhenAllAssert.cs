using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.HasEnumerable;

public class WhenAllAssert : Spec
{
    [Fact]
    public void GivenEmpty_ThenDoesNotThrow() 
        => Zero<int>().Has().All(it => it.Is(1)).And.Is().Empty();

    [Fact]
    public void GivenAllSatisfyCondition_ThenDoesNotThrow()
    {
        int[] arr = [1, 2];
        arr.Has().All(it => it.Is().Not().LessThan(1)).And.Is().NotEmpty();
    }

    [Fact]
    public void GivenConditionNotSatisfiedForAll_ThenGetException()
    {
        int[] arr = [1, 3];
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => arr.Has().All(it => it.Is().LessThan(3)));
        ex.Message.Is($"Arr has all it.Is().LessThan(3)");
        ex.InnerException.Message.Is($"Expected arr to have all elements satisfying the assertion but found [1, 3]");
        ex.InnerException.InnerException.Message.Is($"Expected it to be less than 3 but found 3");
    }
}