using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.Given;

public class WhenGivenList : StaticSpec<List<int>>
{
    [Fact] public void ThenReturnList() => Given(new List<int> { 1, 2, 3 }).When(_ => _).Then().Result.Has().Count(3);
}

public class WhenGivenAList : StaticSpec<List<int>>
{
    [Fact] public void ThenReturnTheList() => Given(A<List<int>>()).When(_ => _).Then().Result.Is(The<List<int>>());
}

public class WhenUsingTwoInts : SubjectSpec<MyListService, List<int>>
{
    [Fact] public void ThenReturnTwoInts() => Given(Two<int>().ToList()).When(_ => _.List).Then().Result.Has().Count(2);
}

public class MyListService
{
    private readonly List<int> _list;
    public MyListService(List<int> list) => _list = list;
    public List<int> List => _list;
}