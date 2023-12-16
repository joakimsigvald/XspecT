using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenSetupValueWithDefault : SubjectSpec<MyService, int>
{
    private const int _defaltId = 1;

    [Fact]
    public void GivenDefaultNotOverridden()
        => GivenDefault(_defaltId)
        .And<IMyRepository>().That(_ => _.GetNextId()).Returns(ASecond<int>)
        .When(_ => _.GetNextId())
        .Then().Result.Is(_defaltId);

    [Fact]
    public void GivenDefaultIsOverridden()
        => Given<IMyRepository>().That(_ => _.GetNextId()).Returns(ASecond<int>)
        .When(_ => _.GetNextId())
        .GivenDefault(_defaltId)
        .And().That(() => ASecond(2))
        .Then().Result.Is(2);
}