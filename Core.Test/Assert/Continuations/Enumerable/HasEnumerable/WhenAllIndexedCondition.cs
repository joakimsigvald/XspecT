﻿using XspecT.Assert;
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
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => arr.Has().All((it, i) => it == i + 1));
        ex.Message.Is($"Arr has all (it, i) => it == i + 1");
        ex.InnerException.Message.Is($"Expected arr to have all elements satisfying the condition but found [1, 3]");
    }
}