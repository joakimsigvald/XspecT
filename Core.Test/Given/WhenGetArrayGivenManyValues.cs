using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGetArrayGivenManyValues : SubjectSpec<MyService, int[]>
{
    public WhenGetArrayGivenManyValues() => When(_ => _.GetIds());

    [Fact]
    public void ThenCanUseTwoValuesGivenSeparatelyFromMock()
        => Given(Two<int>()).Then().Result.Is(Two<int>());

    [Fact]
    public void ThenDoNotUseTwoValuesGivenInDifferentSetup()
        => Given<MyModel>(_ => _.Values = Two<int>()).Then().Result.Is().Empty();
}