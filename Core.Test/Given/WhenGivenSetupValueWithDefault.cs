using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenSetupValueWithDefault : Spec<MyService, int>
{
    private const int _defaultId = 1;

    [Fact]
    public void GivenDefaultNotOverridden()
        => Given(_defaultId)
        .And<IMyRepository>().That(_ => _.GetNextId()).Returns(ASecond<int>)
        .When(_ => _.GetNextId())
        .Then().Result.Is(_defaultId);

    [Fact]
    public void GivenDefaultIsOverridden()
        => Given<IMyRepository>().That(_ => _.GetNextId()).Returns(ASecond<int>)
        .When(_ => _.GetNextId())
        .Given(_defaultId)
        .And().ASecond(2)
        .Then().Result.Is(2);
}