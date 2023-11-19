using XspecT.Assert;

namespace XspecT.Test.Pipeline;

public class AfterWhenBefore : SubjectSpec<MyStateService, int>
{
    [Fact]
    public void BeforeIsExecutedAfterWhen()
        => When(_ => ++_.Counter).Before(_ => _.Counter--).Then().Result.Is(1);

    [Fact]
    public void AfterIsExecutedBeforeWhen()
        => When(_ => ++_.Counter).After(_ => _.Counter++).Then().Result.Is(2);

    [Fact]
    public void GivenWhenExecutedTwice_ThenThrowSetupFailed()
        => Xunit.Assert.Throws<SetupFailed>(() => When(_ => ++_.Counter).When(_ => _.Counter--));

    [Fact]
    public void GiveAfterExecutedTwice_ThenThrowSetupFailed()
        => Xunit.Assert.Throws<SetupFailed>(() => After(_ => ++_.Counter).After(_ => _.Counter--));

    [Fact]
    public void GivenBeforeExecutedTwice_ThenThrowSetupFailed()
        => Xunit.Assert.Throws<SetupFailed>(() => Before(_ => ++_.Counter).Before(_ => _.Counter--));

    [Fact]
    public void GivenCallThenBeforeWhen_ThenThrowSetupFailed()
        => Xunit.Assert.Throws<SetupFailed>(() => Then().Throws<Exception>());

    [Fact]
    public void WhenBeforeGivenAfter()
        => When(_ => ++_.Counter).Before(_ => ++_.Counter).Given(1).After(_ => _.Counter++).Then().Result.Is(2);
}