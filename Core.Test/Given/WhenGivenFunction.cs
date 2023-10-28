using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.Given;

public class WhenGivenFunction : SubjectSpec<MyService, DateTime>
{
    [Fact]
    public void UsingFunction_CanBeUsedAsDefaultFunction()
        => Given(A<DateTime>).When(_ => _.GetTime())
        .Then().Result.Is(The<DateTime>());

    [Fact]
    public void UsingValue_CanBeUsedForDefaultFunction()
        => Given(A<DateTime>()).When(_ => _.GetTime())
        .Then().Result.Is(The<DateTime>());
}