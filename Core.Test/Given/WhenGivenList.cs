using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenList : Spec<object, List<int>>
{
    [Fact] public void ThenReturnList() 
        => When(_ => new List<int> { 1, 2, 3 }).Then().Result.Has().Count(3);
}

public class WhenGivenAList : Spec<object, List<int>>
{
    [Fact] public void ThenReturnTheList() => When(_ => A<List<int>>()).Then().Result.Is(The<List<int>>());
}