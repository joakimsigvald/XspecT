namespace XspecT.Test.Given;

public class WhenGivenList : Spec<List<int>>
{
    [Fact] public void ThenReturnList() 
        => When(_ => new List<int> { 1, 2, 3 }).Then().Result.Has().Count(3);
}

public class WhenGivenAList : Spec<List<int>>
{
    [Fact] public void ThenReturnTheList() => When(_ => A<List<int>>()).Then().Result.Is(The<List<int>>());
}