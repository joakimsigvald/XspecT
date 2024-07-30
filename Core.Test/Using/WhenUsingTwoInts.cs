using XspecT.Test.Given;

namespace XspecT.Test.Using;

public class WhenUsingTwoInts : Spec<MyListService, List<int>>
{
    [Fact] public void ThenReturnTwoInts() => Given(Two<int>().ToList()).When(_ => _.List).Then().Result.Has().Count(2);
}