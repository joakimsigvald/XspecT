﻿using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.HasEnumerable;

public class WhenThreeItemsCondition : Spec
{
    [Fact]
    public void GivenThreeItemsConditionSatisfied_ThenDoesNotThrow()
    {
        int[] arr = [1, 1, 1];
        arr.Has().ThreeItems(it => it == 1).And.Is().Not().Empty();
    }

    [Fact]
    public void GivenThreeSatisfyingElementOutOfThree_ThenDoesNotThrow()
    {
        int[] arr = [1, 1, 1, 999];
        arr.Has().ThreeItems(it => it == 1);
    }

    [Fact]
    public void GivenThreeItemsConditionSatisfied_AndVerifyIt_ThenDoesNotThrow()
    {
        int[] arr = [1, 1, 1];
        arr.Has().ThreeItems(it => it == 1).That.first.Is().LessThan(200);
    }

    [Fact]
    public void GivenFourSatisfyingItems_ThenGetException()
    {
        int[] arr = [1, 2, 3, 4];
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => arr.Has().ThreeItems(it => it > 0));
        ex.Message.Is($"Arr has three items it > 0");
        ex.InnerException.Message.Is($"Expected arr to have three items satisfying the condition but found 4: [1, 2, 3, 4]");
    }
}