﻿using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenUsingTwoInts : SubjectSpec<MyListService, List<int>>
{
    [Fact] public void ThenReturnTwoInts() => Given(Two<int>().ToList()).When(_ => _.List).Then().Result.Has().Count(2);
}