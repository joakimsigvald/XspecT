using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.HasEnumerable;

public class WhenFiveItemsCondition : Spec
{
    [Fact]
    public void GivenFiveItemsConditionSatisfied_AndVerifyIt_ThenDoesNotThrow()
    {
        int[] arr = [1, 2, 3, 4, 5];
        arr.Has().FiveItems(it => it > 0).That.fifth.Is(5);
    }

    [Fact]
    public void GivenSixSatisfyingItems_ThenGetException()
    {
        int[] arr = [1, 2, 3, 4, 5, 6];
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => arr.Has().FiveItems(it => it > 0));
        ex.Message.Is($"Arr has five items it > 0");
        ex.InnerException.Message.Is($"Expected arr to have five items satisfying the condition but found 6: [1, 2, 3, 4, 5, 6]");
    }
}