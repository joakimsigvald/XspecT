﻿using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.HasEnumerable;

public class WhenTwoItems : Spec
{
    [Fact]
    public void GivenTwoItems_ThenDoesNotThrow()
    {
        Two<int>().Has().TwoItems().And.Is().Not().Empty();
        Specification.Is(
            """
            Two int has two items
                and is not empty
            """);
    }

    [Fact]
    public void GivenTwoItems_AndVerifyIt()
    {
        var (first, second) = Two<int>().Has().TwoItems().That;
        first.Is(TheFirst<int>());
        second.Is(TheSecond<int>());
        Specification.Is("""
            Two int has two items that first is the first int
            Second is the second int
            """);
    }

    [Fact]
    public void GivenEmpty_ThenGetException()
    {
        int[] arr = Zero<int>();
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => arr.Has().TwoItems());
        ex.Message.Is($"Arr has two items");
        ex.InnerException.Message.Is($"Expected arr to have two items but found 0: []");
    }

    [Fact]
    public void GivenThreeElements_ThenGetException()
    {
        int[] arr = [1, 2, 3];
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => arr.Has().TwoItems());
        ex.Message.Is($"Arr has two items");
        ex.InnerException.Message.Is($"Expected arr to have two items but found 3: [1, 2, 3]");
    }
}