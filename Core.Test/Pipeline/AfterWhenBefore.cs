using XspecT.Assert;

namespace XspecT.Test.Pipeline;

public class AfterWhenBefore : Spec<MyStateService, int>
{
    [Fact]
    public void BeforeIsExecutedAfterWhen()
        => When(_ => ++_.Counter).Before(_ => _.Counter--).Then().Result.Is(1);

    [Fact]
    public void AfterIsExecutedBeforeWhen()
        => When(_ => ++_.Counter).After(_ => _.Counter++).Then().Result.Is(2);

    [Fact]
    public void FirstAfterIsExecutedAfterSecondAfterBeforeWhen()
        => When(_ => _.Counter *= 2)
        .After(_ => _.Counter = 3)
        .After(_ => _.Counter = 5)
        .Then().Result.Is(6);

    [Fact]
    public void GivenWhenExecutedTwice_ThenThrowSetupFailed()
        => Xunit.Assert.Throws<SetupFailed>(() => When(_ => ++_.Counter).When(_ => _.Counter--));

    [Fact]
    public void GivenBeforeExecutedTwice_BothAreExecuted()
        => When(_ => _.Counter = 1).Before(_ => _.Counter = 3).Before(_ => _.Counter = 2)
        .Then().Result.Is(1);

    [Fact]
    public void GivenCallThenBeforeWhen_ThenThrowSetupFailed()
        => Xunit.Assert.Throws<SetupFailed>(() => Then().Throws<Exception>());

    [Fact]
    public void WhenBeforeGivenAfter()
        => When(_ => ++_.Counter).Before(_ => ++_.Counter).Given(1).After(_ => _.Counter++).Then().Result.Is(2);
}