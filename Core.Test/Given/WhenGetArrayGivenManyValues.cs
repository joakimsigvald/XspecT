using XspecT.Fixture;

namespace XspecT.Test.Given;

public class WhenGetArrayGivenManyValues : SubjectSpec<MyService, int[]>
{
    [Fact]
    public void ThenCanUseTwoValues()
        => Given<IMyRepository>().That(_ => _.GetIds()).Returns(Two<int>)
        .When(_ => _.GetNextId())
        .Given(1, 2, 3)
        .Then().Result.Equals(new[] { 1, 2 });

    [Fact]
    public void ThenCanUseFiveValues()
        => Given<IMyRepository>().That(_ => _.GetIds()).Returns(Five<int>)
        .When(_ => _.GetNextId())
        .Given(1, 2, 3, 3, 4)
        .Then().Result.Equals(new[] { 1, 2, 3, 4, 5 });
}