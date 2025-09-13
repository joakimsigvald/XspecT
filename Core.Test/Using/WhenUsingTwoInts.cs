﻿using XspecT.Assert;
using XspecT.Test.TestData;

namespace XspecT.Test.Using;

public class WhenUsingTwoInts : Spec<MyListService, List<int>>
{
    [Fact]
    public void ThenReturnTwoInts()
    {
        Given(Two<int>().ToList()).When(_ => _.List).Then().Result.Has().Count(2);
        Specification.Is(
            """
            Given two int's ToList()
            When _.List
            Then Result has count 2
            """);
    }
}