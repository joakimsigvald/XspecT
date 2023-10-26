using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.Given;

public class WhenGivenFunction : SubjectSpec<MyService, DateTime>
{
    [Fact]
    public void ThenItCannotBeUsedAsDefaultValue()
        => Given(A<DateTime>).When(_ => _.GetTime())
        .Then().Result.Is().Not(The<DateTime>());

    [Fact]
    public void UsingFunction_CanBeUsedAsDefaultFunction()
        => Using(A<DateTime>).When(_ => _.GetTime())
        .Then().Result.Is(The<DateTime>());

    [Fact]
    public void UsingValue_CanBeUsedForDefaultFunction()
        => Using(A<DateTime>()).When(_ => _.GetTime())
        .Then().Result.Is(The<DateTime>());
}