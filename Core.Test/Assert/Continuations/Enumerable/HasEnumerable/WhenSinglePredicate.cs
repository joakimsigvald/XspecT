using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Continuations.Enumerable.HasEnumerable;

public class WhenSinglePredicate : Spec
{
    [Fact]
    public void GivenSinglePredicateSatisfied_ThenDoesNotThrow()
    {
        int[] arr = [123];
        arr.Has().Single(it => it == 123).And.Is().NotEmpty();
    }

    [Fact]
    public void GivenOneSatisfyingElementOutOfTwo_ThenDoesNotThrow()
    {
        int[] arr = [123, 999];
        arr.Has().Single(it => it == 123);
    }

    [Fact]
    public void GivenSinglePredicateSatisfied_AndVerifyIt_ThenDoesNotThrow()
    {
        int[] arr = [123];
        arr.Has().Single(it => it == 123).That.Is().LessThan(200);
    }

    [Fact]
    public void GivenOneSatisfyingElementOutOfTwo_AndVerifyIt_ThenDoesNotThrow()
    {
        int[] arr = [123, 999];
        arr.Has().Single(it => it == 123).That.Is().GreaterThan(100);
    }

    [Fact]
    public void GivenSinglePredicateNotSatisfied_ThenDoesNotThrow()
    {
        int[] arr = [999];
        var ex = Xunit.Assert.Throws<XunitException>(() => arr.Has().Single(it => it == 123));
        ex.Message.Is($"Arr has single it = 123");
        ex.InnerException.Message.Is($"Expected arr to have single element satisfying the condition but found [999]");
    }

    [Fact]
    public void GivenEmpty_ThenGetException()
    {
        int[] arr = Zero<int>();
        var ex = Xunit.Assert.Throws<XunitException>(() => arr.Has().Single(it => it == 123));
        ex.Message.Is($"Arr has single it = 123");
        ex.InnerException.Message.Is($"Expected arr to have single element satisfying the condition but found empty");
    }

    [Fact]
    public void GivenTwoSatisfyingElements_ThenGetException()
    {
        int[] arr = [1, 3];
        var ex = Xunit.Assert.Throws<XunitException>(() => arr.Has().Single(it => it > 0));
        ex.Message.Is($"Arr has single it > 0");
        ex.InnerException.Message.Is($"Expected arr to have single element satisfying the condition but found [1, 3]");
    }
}