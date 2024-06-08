using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenFunction : Spec<MyService, DateTime>
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