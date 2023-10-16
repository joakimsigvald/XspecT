using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.Given;

public class WhenGetValueGivenManyValues : SubjectSpec<MyService, int>
{
    [Fact]
    public void ThenCanUseFirstValue()
        => Given<IMyRepository>().That(_ => _.GetNextId()).Returns(The<int>)
        .When(_ => _.GetNextId())
        .Given(1, 2)
        .Then().Result.Is(1);

    [Fact]
    public void ThenCanUseSecondValue()
        => Given<IMyRepository>().That(_ => _.GetNextId()).Returns(TheSecond<int>)
        .When(_ => _.GetNextId())
        .Given(1, 2)
        .Then().Result.Is(2);

    [Fact]
    public void ThenCanUseThirdValue()
        => Given<IMyRepository>().That(_ => _.GetNextId()).Returns(TheThird<int>)
        .When(_ => _.GetNextId())
        .Given(1, 2, 3)
        .Then().Result.Is(3);

    [Fact]
    public void ThenCanUseFourthValue()
        => Given<IMyRepository>().That(_ => _.GetNextId()).Returns(TheFourth<int>)
        .When(_ => _.GetNextId())
        .Given(1, 2, 3, 4)
        .Then().Result.Is(4);

    [Fact]
    public void ThenCanUseFifthValue()
        => Given<IMyRepository>().That(_ => _.GetNextId()).Returns(TheFifth<int>)
        .When(_ => _.GetNextId())
        .Given(1, 2, 3, 4, 5)
        .Then().Result.Is(5);
}