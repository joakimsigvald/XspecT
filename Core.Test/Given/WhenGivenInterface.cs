using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.Given;

public class WhenGivenInterface : SubjectSpec<MyService, string>
{
    [Fact]
    public void ThenUseValueInPipeline()
        => Using<IMySettings>(new MySettings { ConnectionString = ASecond<string>()})
        .When(_ => _.GetConnectionString())
        .Then().Result.Is(TheSecond<string>());
}